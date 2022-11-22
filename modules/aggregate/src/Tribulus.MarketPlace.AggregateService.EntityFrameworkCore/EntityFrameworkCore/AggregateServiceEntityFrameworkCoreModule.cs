using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.AggregateService.EntityFrameworkCore;

[DependsOn(
    typeof(AggregateServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class AggregateServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<AggregateServiceDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
        });
    }
}
