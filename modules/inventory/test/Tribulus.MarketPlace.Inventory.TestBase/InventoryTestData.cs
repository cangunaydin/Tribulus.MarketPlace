using System;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace.Inventory
{
    public class InventoryTestData : ISingletonDependency
    {
        public Guid UserJohnId { get; } = Guid.NewGuid();

        public Guid ProductIphone13Id { get; } = Guid.NewGuid();

        public Guid ProductIphone13ProId { get; } = Guid.NewGuid();
        public Guid ProductIphone14Id { get; } = Guid.NewGuid();

        public Guid OrderItemId { get; } = Guid.NewGuid();

        public Guid OrderItem2Id { get; } = Guid.NewGuid();

        public Guid OrderId { get; } = Guid.NewGuid();


    }
}
