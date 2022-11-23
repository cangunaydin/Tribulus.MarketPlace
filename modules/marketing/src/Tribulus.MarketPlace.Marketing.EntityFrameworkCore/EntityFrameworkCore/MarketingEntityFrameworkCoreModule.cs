using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Marketing.EntityFrameworkCore.Repositories;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Marketing.EntityFrameworkCore;

[DependsOn(
    typeof(MarketingDomainModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class MarketingEntityFrameworkCoreModule : AbpModule
{

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        MarketingEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MarketingDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */

            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            
            options.AddDefaultRepositories(includeAllEntities: true);
            options.AddRepository<Product, ProductRepository>();
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure<MarketingDbContext>(c =>
            {
                c.UseSqlServer(b =>
                {
                    b.MigrationsHistoryTable("__MarketingService_Migrations");
                });
            });
        });
    }

}
