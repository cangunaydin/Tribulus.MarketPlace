using System;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Products;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Tribulus.MarketPlace.Orders
{
    public class OrderManager : DomainService
    {
        private readonly IRepository<Product, Guid> _productRepository;
        public OrderManager(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task PlaceOrderAsync(Order order)
        {
            var productIds=order.OrderItems.Select(x => x.ProductId).ToList();
            var products=await _productRepository.GetListAsync(o => productIds.Contains(o.Id));
            foreach (var product in products)
            {
                var orderItem = order.OrderItems.First(o => o.ProductId == product.Id);
                if (product.StockCount < orderItem.Quantity)
                    throw new BusinessException(MarketPlaceDomainErrorCodes.ProductHasNotEnoughStock);
            }
            order.PlaceOrder();
        }
    }
}
