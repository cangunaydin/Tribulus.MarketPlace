using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Sales;
using Tribulus.MarketPlace.Marketing.EntityFrameworkCore;
using Tribulus.MarketPlace.Sales.EntityFrameworkCore;
using Tribulus.MarketPlace.Inventory.EntityFrameworkCore;
using Tribulus.MarketPlace.Admin.Marketing.EntityFrameworkCore;
using Tribulus.MarketPlace.Admin.Sales.EntityFrameworkCore;
using Tribulus.MarketPlace.Admin.Inventory.EntityFrameworkCore;

namespace Tribulus.MarketPlace.EntityFrameworkCore;

[DependsOn(
    typeof(MarketPlaceDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
[DependsOn(typeof(MarketingEntityFrameworkCoreModule))]
    [DependsOn(typeof(SalesEntityFrameworkCoreModule))]
    [DependsOn(typeof(InventoryEntityFrameworkCoreModule))]
    public class MarketPlaceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        MarketPlaceEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MarketPlaceDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories();

        });

        Configure<AbpDbContextOptions>(options =>
        {
                /* The main point to change your DBMS.
                 * See also MarketPlaceMigrationsDbContextFactory for EF Core tooling. */
            options.UseSqlServer();
        });

        Configure<AbpEntityOptions>(options =>
        {
            options.Entity<Order>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query
                => query
                .Include(f => f.OrderItems);
                
            });
        });

    }
}
