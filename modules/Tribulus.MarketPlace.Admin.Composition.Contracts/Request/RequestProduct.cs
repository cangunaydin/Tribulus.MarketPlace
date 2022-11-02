using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;

namespace Tribulus.MarketPlace.Admin.Products
{
    public interface RequestProduct
    {
        Guid RequestId { get; }

        ProductCompositionSaveDto Product { get; }

        [ModuleInitializer]
        internal static void Init()
        {
            GlobalTopology.Send.UseCorrelationId<RequestProduct>(x => x.Product.Id);
        }
    }
}