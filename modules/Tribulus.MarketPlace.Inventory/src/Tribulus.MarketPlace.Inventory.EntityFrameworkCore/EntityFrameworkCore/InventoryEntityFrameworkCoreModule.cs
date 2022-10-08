using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Inventory.EntityFrameworkCore;

[DependsOn(
    typeof(InventoryDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class InventoryEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<InventoryDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
        });
    }
}
