using Microsoft.Extensions.Logging;
using System;
using Tribulus.MarketPlace.AggregateService.EntityFrameworkCore;
using Tribulus.MarketPlace.Shared.Hosting.Microservices.DbMigrations.EfCore;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Tribulus.MarketPlace.AggregateService.DbMigrations;

public class AggregateServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<AggregateServiceDbContext>
{
    public AggregateServiceDatabaseMigrationChecker(
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
        AggregateServiceDbProperties.ConnectionStringName)
    {
    }
}
