using MassTransit;
using MassTransit.Courier.Contracts;
using MassTransit.Middleware;
using System;
using Tribulus.MarketPlace.Admin.Models;

namespace Tribulus.MarketPlace.Admin.StateMachine
{
    public class ProductSagaDefinition : SagaDefinition<ProductState> 
    {
        public ProductSagaDefinition()
        {
            var partitionCount = 32;

            ConcurrentMessageLimit = partitionCount;
        }
        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<ProductState> sagaConfigurator)
        {
            if (endpointConfigurator is IInMemoryReceiveEndpointConfigurator inMemory)
            {
                endpointConfigurator.ConfigureConsumeTopology = false;
                inMemory.Bind<RequestProduct>();
            }

            var partitionCount = ConcurrentMessageLimit ?? Environment.ProcessorCount * 4;

            IPartitioner partitioner = new Partitioner(partitionCount, new Murmur3UnsafeHashGenerator());

            endpointConfigurator.UsePartitioner<RequestProduct>(partitioner, x => x.Message.Product.ProductId);
            endpointConfigurator.UsePartitioner<RoutingSlipCompleted>(partitioner, x => x.Message.TrackingNumber);
            endpointConfigurator.UsePartitioner<RoutingSlipFaulted>(partitioner, x => x.Message.TrackingNumber);
        }
    }
}
