using System;

namespace Tribulus.MarketPlace.Admin.Models
{
    public interface RequestProduct
    {
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        //[ModuleInitializer]
        //internal static void Init()
        //{
        //    GlobalTopology.Send.UseCorrelationId<RequestProduct>(x => x.Product.ProductId);
        //}
    }
}
