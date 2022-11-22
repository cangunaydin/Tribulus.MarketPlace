using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Marketing.EntityFrameworkCore.Repositories;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Marketing.EntityFrameworkCore;

[DependsOn(
    typeof(MarketingDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class MarketingEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MarketingDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<Product, ProductRepository>();
        });
    }
}
