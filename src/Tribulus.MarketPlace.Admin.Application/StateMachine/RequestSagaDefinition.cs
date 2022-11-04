﻿using MassTransit;
using MassTransit.Components;
using MassTransit.Contracts;
using MassTransit.Middleware;
using System;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace.Admin.StateMachine
{
    public class RequestSagaDefinition :
        SagaDefinition<RequestState>, ITransientDependency
    {
        public RequestSagaDefinition()
        {
            var partitionCount = 64;

            ConcurrentMessageLimit = partitionCount;
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<RequestState> sagaConfigurator)
        {
            var partitionCount = ConcurrentMessageLimit ?? Environment.ProcessorCount * 4;

            IPartitioner partitioner = new Partitioner(partitionCount, new Murmur3UnsafeHashGenerator());

            endpointConfigurator.UsePartitioner<RequestStarted>(partitioner, x => x.Message.RequestId);
            endpointConfigurator.UsePartitioner<RequestCompleted>(partitioner, x => x.Message.CorrelationId);
            endpointConfigurator.UsePartitioner<RequestFaulted>(partitioner, x => x.Message.CorrelationId);
        }
    }
}