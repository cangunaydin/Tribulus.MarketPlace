using Microsoft.Extensions.Logging;
using System;
using Tribulus.MarketPlace.Marketing;
using Tribulus.MarketPlace.Marketing.EntityFrameworkCore;
using Tribulus.MarketPlace.Shared.Hosting.Microservices.DbMigrations.EfCore;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Tribulus.MarketPlace.Admin.Marketing.DbMigrations;

public class MarketingServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<MarketingDbContext>
{
    public MarketingServiceDatabaseMigrationChecker(
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
        MarketingDbProperties.ConnectionStringName)
    {
    }
}
