using Microsoft.Extensions.Logging;
using System;
using Tribulus.MarketPlace.Inventory;
using Tribulus.MarketPlace.Inventory.EntityFrameworkCore;
using Tribulus.MarketPlace.Shared.Hosting.Microservices.DbMigrations.EfCore;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Tribulus.MarketPlace.Admin.Inventory.DbMigrations;

public class InventoryServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<InventoryDbContext>
{
    public InventoryServiceDatabaseMigrationChecker(
        ILoggerFactory loggerFactory,
        IUnitOfWorkManager unitOfWorkManager,
        IServiceProvider serviceProvider,
        ICurrentTenant currentTenant,
        IDistributedEventBus distributedEventBus,
        IAbpDistributedLock abpDistributedLock) : base(
        loggerFactory,
        unitOfWorkManager,
        serviceProvider,
        currentTenant,
        distributedEventBus,
        abpDistributedLock,
        InventoryDbProperties.ConnectionStringName)
    {
    }
}
