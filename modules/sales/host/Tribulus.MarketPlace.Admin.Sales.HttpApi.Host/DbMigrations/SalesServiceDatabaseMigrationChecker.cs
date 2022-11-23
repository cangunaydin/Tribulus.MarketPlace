﻿using Microsoft.Extensions.Logging;
using System;
using Tribulus.MarketPlace.Sales;
using Tribulus.MarketPlace.Sales.EntityFrameworkCore;
using Tribulus.MarketPlace.Shared.Hosting.Microservices.DbMigrations.EfCore;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Tribulus.MarketPlace.Admin.Sales.DbMigrations;

public class SalesServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<SalesDbContext>
{
    public SalesServiceDatabaseMigrationChecker(
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
        SalesDbProperties.ConnectionStringName)
    {
    }
}