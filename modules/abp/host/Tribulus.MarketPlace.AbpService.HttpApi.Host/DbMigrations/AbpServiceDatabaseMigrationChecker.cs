using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.AbpService.EntityFrameworkCore;
using Tribulus.MarketPlace.Shared.Hosting.Microservices.DbMigrations.EfCore;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Tribulus.MarketPlace.AbpService.DbMigrations;

public class AbpServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<AbpServiceDbContext>
{
    private readonly AbpServiceDataSeeder _dataSeeder;

    public AbpServiceDatabaseMigrationChecker(
        ILoggerFactory loggerFactory,
        IUnitOfWorkManager unitOfWorkManager,
        IServiceProvider serviceProvider,
        ICurrentTenant currentTenant,
        IDistributedEventBus distributedEventBus,
        IAbpDistributedLock abpDistributedLock,
        AbpServiceDataSeeder dataSeeder) : base(
        loggerFactory,
        unitOfWorkManager,
        serviceProvider,
        currentTenant,
        distributedEventBus,
        abpDistributedLock,
        AbpServiceDbProperties.ConnectionStringName)
    {
        _dataSeeder = dataSeeder;
    }

    public override async Task CheckAndApplyDatabaseMigrationsAsync()
    {
        await base.CheckAndApplyDatabaseMigrationsAsync();
        
        await TryAsync(async () => await _dataSeeder.SeedAsync());
    }
}