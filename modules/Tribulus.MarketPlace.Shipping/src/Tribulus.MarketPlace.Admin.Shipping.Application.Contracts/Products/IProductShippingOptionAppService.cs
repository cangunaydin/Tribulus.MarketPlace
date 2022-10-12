using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Shipping.Products
{
    public interface IProductShippingOptionAppService : IApplicationService
    {
        Task<CreateProdcutShippingOptionDto> CreateAsync(Guid id, CreateProdcutShippingOptionDto input);

        //Task UpdateAsync(Guid id, UpdateProductStockDto input);



    }
}
