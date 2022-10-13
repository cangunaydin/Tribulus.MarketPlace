using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Tribulus.Composition;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Composition
{
    [DependsOn(typeof(AdminCompositionContractsModule),
                typeof(TribulusCompositionModule),
        typeof(AbpAutofacModule))]
    public class AdminCompositionModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var containerBuilder = context.Services.GetContainerBuilder();

            var assembly = typeof(AdminCompositionModule).GetTypeInfo().Assembly;

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