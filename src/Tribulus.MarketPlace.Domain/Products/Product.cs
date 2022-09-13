using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Products.Events;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tribulus.MarketPlace.Products
{
    public class Product:FullAuditedAggregateRoot<Guid>
    {
        public Guid OwnerUserId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; set; }

        public decimal Price { get; private set; }

        public int StockCount { get; private set; }
        public StockState StockState { get; private set; }  



        private Product() { }

        public Product(Guid id, 
            Guid ownerId,
            string name,
            decimal price,
            int stockCount) : base(id)
        {

            OwnerUserId = ownerId;
            UpdateName(name);
            UpdatePrice(price);
            UpdateStockCount(stockCount);
        }

        public void UpdateName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name),ProductConsts.MaxNameLength,ProductConsts.MinNameLength);
            Name = name;
        }
        public void UpdateStockCount(int stockCount)
        {
            if (stockCount<0)
                throw new ArgumentException(nameof(stockCount));

            StockCount = stockCount;
            //ADD an EVENT TO BE PUBLISHED
            AddLocalEvent(
                new StockCountChangedEventData
                {
                    ProductId = Id,
                    NewStockCount = StockCount
                }
            );

        }
        public void UpdatePrice(decimal price)
        {
            Check.Positive(price, nameof(price));
            Price = price;
        }


    }
}
