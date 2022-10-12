using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Shipping.Products
{
    public interface IProductShippingOptionAppService : IApplicationService
    {
        Task<CreateProductShippingOptionDto> CreateAsync(Guid id, CreateProductShippingOptionDto input);

        //Task UpdateAsync(Guid id, UpdateProductStockDto input);



    }
}
