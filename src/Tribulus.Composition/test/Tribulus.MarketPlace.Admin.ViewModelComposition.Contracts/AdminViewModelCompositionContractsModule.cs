using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tribulus.ServiceComposer;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.ViewModelComposition.Contracts;

[DependsOn(typeof(TribulusServiceComposerModule))]
public class AdminViewModelCompositionContractsModule : AbpModule
{
   
}
