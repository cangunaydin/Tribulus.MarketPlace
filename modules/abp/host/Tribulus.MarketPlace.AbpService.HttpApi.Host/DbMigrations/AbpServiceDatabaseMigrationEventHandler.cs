using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tribulus.MarketPlace.AbpService.EntityFrameworkCore;
using Tribulus.MarketPlace.Shared.Hosting.Microservices.DbMigrations;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using Volo.Abp.Uow;

namespace Tribulus.MarketPlace.AbpService.DbMigrations;

public class AbpServiceDatabaseMigrationEventHandler
    : DatabaseMigrationEventHandlerBase<AbpServiceDbContext>,
        IDistributedEventHandler<TenantCreatedEto>,
        IDistributedEventHandler<TenantConnectionStringUpdatedEto>,
        IDistributedEventHandler<ApplyDatabaseMigrationsEto>
{
    private readonly AbpServiceDataSeeder _identityServiceDataSeeder;

    public AbpServiceDatabaseMigrationEventHandler(
        ILoggerFactory loggerFactory,
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager,
        ITenantStore tenantStore,
        ITenantRepository tenantRepository,
        IDistributedEventBus distributedEventBus, 
        AbpServiceDataSeeder identityServiceDataSeeder) : base(
        loggerFactory,
        currentTenant,
        unitOfWorkManager,
        tenantStore,
        tenantRepository,
        distributedEventBus,
        AbpServiceDbProperties.ConnectionStringName)
    {
        _identityServiceDataSeeder = identityServiceDataSeeder;
    }

    public async Task HandleEventAsync(ApplyDatabaseMigrationsEto eventData)
    {
        if (eventData.DatabaseName != DatabaseName)
        {
            return;
        }

        try
        {
            var schemaMigrated = await MigrateDatabaseSchemaAsync(eventData.TenantId);
            await _identityServiceDataSeeder.SeedAsync(
                tenantId: eventData.TenantId,
                adminEmail: AbpServiceDbProperties.DefaultAdminEmailAddress,
                adminPassword: AbpServiceDbProperties.DefaultAdminPassword
            );

            if (eventData.TenantId == null && schemaMigrated)
            {
                /* Migrate tenant databases after host migration */
                await QueueTenantMigrationsAsync();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorOnApplyDatabaseMigrationAsync(eventData, ex);
        }
    }

    public async Task HandleEventAsync(TenantCreatedEto eventData)
    {
        try
        {
            await MigrateDatabaseSchemaAsync(eventData.Id);
            await _identityServiceDataSeeder.SeedAsync(
                tenantId: eventData.Id,
                adminEmail: eventData.Properties.GetOrDefault(IdentityDataSeedContributor.AdminEmailPropertyName) ?? AbpServiceDbProperties.DefaultAdminEmailAddress,
                adminPassword: eventData.Properties.GetOrDefault(IdentityDataSeedContributor.AdminPasswordPropertyName) ?? AbpServiceDbProperties.DefaultAdminPassword
            );
        }
        catch (Exception ex)
        {
            await HandleErrorTenantCreatedAsync(eventData, ex);
        }
    }

    public async Task HandleEventAsync(TenantConnectionStringUpdatedEto eventData)
    {
        if (eventData.ConnectionStringName != DatabaseName && eventData.ConnectionStringName != ConnectionStrings.DefaultConnectionStringName ||
            eventData.NewValue.IsNullOrWhiteSpace())
        {
            return;
        }

        try
        {
            await MigrateDatabaseSchemaAsync(eventData.Id);
            await _identityServiceDataSeeder.SeedAsync(
                tenantId: eventData.Id,
                adminEmail: AbpServiceDbProperties.DefaultAdminEmailAddress,
                adminPassword: AbpServiceDbProperties.DefaultAdminPassword
            );

            /* You may want to move your data from the old database to the new database!
             * It is up to you. If you don't make it, new database will be empty
             * (and tenant's admin password is reset to IdentityServiceDbProperties.DefaultAdminPassword). */
        }
        catch (Exception ex)
        {
            await HandleErrorTenantConnectionStringUpdatedAsync(eventData, ex);
        }
    }
}
