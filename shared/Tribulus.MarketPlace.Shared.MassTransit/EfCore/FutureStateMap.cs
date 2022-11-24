﻿using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tribulus.MarketPlace.EfCore;

public class FutureStateMap :
 SagaClassMap<FutureState>
{
    protected override void Configure(EntityTypeBuilder<FutureState> entity, ModelBuilder model)
    {
        entity.Property(x => x.CurrentState);

        entity.Property(x => x.Created);
        entity.Property(x => x.Completed);
        entity.Property(x => x.Faulted);

        entity.Property(x => x.Location);

        entity.Property(x => x.Command).HasConversion(new JsonValueConverter<FutureMessage>())
            .Metadata.SetValueComparer(new JsonValueComparer<FutureMessage>());
        entity.Property(x => x.Pending)
            .HasConversion(new JsonValueConverter<HashSet<Guid>>())
            .Metadata.SetValueComparer(new JsonValueComparer<HashSet<Guid>>());
        entity.Property(x => x.Results)
            .HasConversion(new JsonValueConverter<Dictionary<Guid, FutureMessage>>())
            .Metadata.SetValueComparer(new JsonValueComparer<Dictionary<Guid, FutureMessage>>());
        entity.Property(x => x.Faults)
            .HasConversion(new JsonValueConverter<Dictionary<Guid, FutureMessage>>())
            .Metadata.SetValueComparer(new JsonValueComparer<Dictionary<Guid, FutureMessage>>());

        entity.Property(x => x.Subscriptions)
            .HasConversion(new JsonValueConverter<HashSet<FutureSubscription>>())
            .Metadata.SetValueComparer(new JsonValueComparer<HashSet<FutureSubscription>>());
        entity.Property(x => x.Variables)
            .HasConversion(new JsonValueConverter<Dictionary<string, object>>())
            .Metadata.SetValueComparer(new JsonValueComparer<Dictionary<string, object>>());
    }
}
