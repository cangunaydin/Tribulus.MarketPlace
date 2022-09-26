using System;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace
{
    public class MarketPlaceTestData : ISingletonDependency
    {
        public Guid UserAdminId { get; internal set; }
        public string UserAdminUserName { get; } = "admin";

        public Guid UserJohnId { get; } = Guid.NewGuid();
        public string UserJohnUserName { get; } = "john";

        public Guid ProductIphone13Id { get; } = Guid.NewGuid();
        public string ProductIphone13Name { get; } = "Iphone 13";


        public Guid ProductIphone13ProId { get; } = Guid.NewGuid();
        public string ProductIphone13ProName { get; } = "Iphone 13 Pro";
        public string ProductIphone13ProDescription { get; } = "Pro And Beyond";
        public decimal ProductIphone13ProPrice { get; } = 1299;
        public int ProductIphone13StockCount { get; } = 150;

        public Guid ProductIphone14Id { get; } = Guid.NewGuid();
        public string ProductIphone14Name { get; } = "Iphone 14";
        public string ProductIphone14Description { get; } = "Iphone 14, available starting 9.16";
        public decimal ProductIphone14Price { get; } = 1099;
        public int ProductIphone14StockCount { get; } = 150;



        public Guid Order1Id { get; } = Guid.NewGuid();
        public string Order1Name { get; } = "test-order-1";

        public Guid Order2Id { get; } = Guid.NewGuid();
        public string Order2Name { get; } = "test-update-order-data";
        public Guid Order1Item1Id { get; } = Guid.NewGuid();
        public int OrderItem1Quantity { get; } = 10;

        public Guid Order2Item1Id { get; } = Guid.NewGuid();
        public int OrderItem2Quantity { get; } = 5;

        public Guid Order1OwnerUserId { get; } = Guid.NewGuid();
    }
}
