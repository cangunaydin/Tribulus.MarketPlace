using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Shipping.EntityFrameworkCore.Repositories;
using Tribulus.MarketPlace.Shipping.Products;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Shipping.EntityFrameworkCore;

[DependsOn(
    typeof(ShippingDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class ShippingEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ShippingDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<ProductShippingOptions, ProductShippingOptionRepository>();
        });
    }
}
