using Dapr.Actors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Products;

namespace Tribulus.MarketPlace.AggregateService.Actors
{
    public interface IProductProcessActor : IActor
    {
        Task Submit(Guid productId, CreateProductAggregateDto product);
    }
}
