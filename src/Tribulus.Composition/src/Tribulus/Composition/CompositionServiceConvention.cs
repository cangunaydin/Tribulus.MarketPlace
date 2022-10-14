using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Conventions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.DependencyInjection;

namespace Tribulus.Composition.Tribulus.Composition
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IAbpServiceConvention), typeof(AbpServiceConvention))]
    public class CompositionServiceConvention : AbpServiceConvention
    {
        private readonly CompositionOptions _compositionOptions;
        private readonly IServiceProvider _serviceProvider;
        public CompositionServiceConvention(IOptions<AbpAspNetCoreMvcOptions> options,
            IConventionalRouteBuilder conventionalRouteBuilder,
            CompositionOptions compositionOptions,
            IServiceProvider serviceProvider) : base(options, conventionalRouteBuilder)
        {
            _compositionOptions = compositionOptions;
            _serviceProvider = serviceProvider;
        }

        protected override void ApplyForControllers(ApplicationModel application)
        {

            CreateCompositionRoutes();

            UpdateCompositionControllers(application);
            RemoveDuplicateControllers(application);

            foreach (var controller in GetControllers(application))
            {
                var controllerType = controller.ControllerType.AsType();

                var configuration = GetControllerSettingOrNull(controllerType);

                //TODO: We can remove different behaviour for ImplementsRemoteServiceInterface. If there is a configuration, then it should be applied!
                //TODO: But also consider ConventionalControllerSetting.IsRemoteService method too..!

                if (ImplementsRemoteServiceInterface(controllerType))
                {
                    controller.ControllerName = controller.ControllerName.RemovePostFix(ApplicationService.CommonPostfixes);
                    configuration?.ControllerModelConfigurer?.Invoke(controller);
                    ConfigureRemoteService(controller, configuration);
                }
                else
                {
                    var remoteServiceAttr = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(controllerType.GetTypeInfo());
                    if (remoteServiceAttr != null && remoteServiceAttr.IsEnabledFor(controllerType))
                    {
                        ConfigureRemoteService(controller, configuration);
                    }
                }
            }
        }
        protected virtual void UpdateCompositionControllers(ApplicationModel application)
        {
            var compositionControllers = application
               .Controllers
               .Where(o => IsComposition(o.ControllerType))
               .ToList();

            var updatedCompositionControllers = new List<ControllerModel>();
            foreach (var compositionRoute in _compositionOptions.CompositionRouteRegistry)
            {
                var compositionHandler = compositionRoute.Handlers.First();
                var controllerForRoute = compositionControllers
                                        .Where(o => o.ControllerType == compositionHandler.ComponentType)
                                        .FirstOrDefault();

                var newControllerModel = new ControllerModel(controllerForRoute);
                newControllerModel.Actions.RemoveAll(o => o.ActionMethod != compositionHandler.MethodInfo);
                updatedCompositionControllers.Add(newControllerModel);

            }

            //remove old controllers and add new ones.
            application.Controllers.RemoveAll(o => IsComposition(o.ControllerType));
            foreach (var compositionController in updatedCompositionControllers)
            {
                application.Controllers.Add(compositionController);
            }
        }
        protected virtual void CreateCompositionRoutes()
        {
            var compositionHandlers = _serviceProvider.GetServices<ICompositionHandleService>();
            var compositionSubscribers = _serviceProvider.GetServices<ICompositionSubscribeService>();

            _compositionOptions.CreateCompositionRoutes(compositionHandlers, compositionSubscribers);
        }
        protected override IList<ControllerModel> GetControllers(ApplicationModel application)
        {
            return application.Controllers.ToList();
        }
        protected override void ConfigureApiExplorer(ControllerModel controller)
        {
            if (Options.ChangeControllerModelApiExplorerGroupName && controller.ApiExplorer.GroupName.IsNullOrEmpty())
            {
                controller.ApiExplorer.GroupName = controller.ControllerName;
            }

            if (controller.ApiExplorer.IsVisible == null)
            {
                controller.ApiExplorer.IsVisible = IsVisibleRemoteService(controller.ControllerType);
            }

            foreach (var action in controller.Actions)
            {
                ConfigureApiExplorer(action);
            }
        }

        protected override void ConfigureApiExplorer(ActionModel action)
        {
            if (action.ApiExplorer.IsVisible != null)
            {
                return;
            }

            var visible = IsVisibleRemoteServiceMethod(action.ActionMethod);
            if (visible == null)
            {
                return;
            }

            action.ApiExplorer.IsVisible = visible;
        }

        protected override void ConfigureSelector(ControllerModel controller, [CanBeNull] ConventionalControllerSetting configuration)
        {
            RemoveEmptySelectors(controller.Selectors);

            var controllerType = controller.ControllerType.AsType();
            var remoteServiceAtt = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(controllerType.GetTypeInfo());
            if (remoteServiceAtt != null && !remoteServiceAtt.IsEnabledFor(controllerType))
            {
                return;
            }

            if (controller.Selectors.Any(selector => selector.AttributeRouteModel != null))
            {
                return;
            }

            var rootPath = GetRootPathOrDefault(controller.ControllerType.AsType());

            foreach (var action in controller.Actions)
            {
                ConfigureSelector(rootPath, controller.ControllerName, action, configuration);
            }
        }

        protected override void ConfigureSelector(string rootPath, string controllerName, ActionModel action, [CanBeNull] ConventionalControllerSetting configuration)
        {
            RemoveEmptySelectors(action.Selectors);

            var remoteServiceAtt = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(action.ActionMethod);
            if (remoteServiceAtt != null && !remoteServiceAtt.IsEnabledFor(action.ActionMethod))
            {
                return;
            }

            if (!action.Selectors.Any())
            {
                AddAbpServiceSelector(rootPath, controllerName, action, configuration);
            }
            else
            {
                NormalizeSelectorRoutes(rootPath, controllerName, action, configuration);
            }
        }

        protected override void AddAbpServiceSelector(string rootPath, string controllerName, ActionModel action, [CanBeNull] ConventionalControllerSetting configuration)
        {
            var httpMethod = SelectHttpMethod(action, configuration);

            var abpServiceSelectorModel = new SelectorModel
            {
                AttributeRouteModel = CreateAbpServiceAttributeRouteModel(rootPath, controllerName, action, httpMethod, configuration),
                ActionConstraints = { new HttpMethodActionConstraint(new[] { httpMethod }) }
            };

            action.Selectors.Add(abpServiceSelectorModel);
        }

        protected virtual bool IsComposition(Type controllerType)
        {
            return typeof(ICompositionHandleService).GetTypeInfo().IsAssignableFrom(controllerType) || 
                typeof(ICompositionSubscribeService).GetTypeInfo().IsAssignableFrom(controllerType);
        }


        //protected virtual bool ImplementsCompositionHandler(Type controllerType)
        //{
        //    return typeof(ICompositionHandleService).GetTypeInfo().IsAssignableFrom(controllerType);
        //}
        //protected virtual bool ImplementsCompositionSubscriber(Type controllerType)
        //{
        //    return typeof(ICompositionSubscribeService).GetTypeInfo().IsAssignableFrom(controllerType);
        //}


    }
}
