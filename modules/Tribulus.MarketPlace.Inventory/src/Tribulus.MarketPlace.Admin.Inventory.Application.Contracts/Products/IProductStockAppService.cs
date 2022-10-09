using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Inventory.Products
{
    public interface IProductStockAppService : IApplicationService
    {
        Task<ListResultDto<ProductStockDto>> GetListAsync(ProductStockListFilterDto input);

        Task<ProductStockDto> GetAsync(Guid id);

        Task UpdateAsync(Guid id, UpdateProductStockDto input);

        Task<ProductStockDto> CreateAsync(CreateProductStockDto input);
    }
}
