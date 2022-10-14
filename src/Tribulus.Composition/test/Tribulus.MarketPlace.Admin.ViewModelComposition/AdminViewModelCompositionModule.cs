using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tribulus.MarketPlace.Admin.ViewModelComposition.Contracts;
using Tribulus.ServiceComposer;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.ViewModelComposition
{
    [DependsOn(typeof(AdminViewModelCompositionContractsModule))]
    public class AdminViewModelCompositionModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var containerBuilder = context.Services.GetContainerBuilder();

            var assembly = typeof(AdminViewModelCompositionModule).GetTypeInfo().Assembly;

            var typeHandles = assembly.GetTypes()
                .Where(o => o.IsAssignableTo(typeof(ICompositionHandleService)));

            var typeSubscribers = assembly.GetTypes()
                .Where(o => o.IsAssignableTo(typeof(ICompositionSubscribeService)));

            foreach (var type in typeHandles)
            {
                containerBuilder.RegisterType(type).As<ICompositionHandleService>().PropertiesAutowired();
            }

            foreach (var type in typeSubscribers)
            {
                containerBuilder.RegisterType(type).As<ICompositionSubscribeService>().PropertiesAutowired();
            }

        }
    }
}
