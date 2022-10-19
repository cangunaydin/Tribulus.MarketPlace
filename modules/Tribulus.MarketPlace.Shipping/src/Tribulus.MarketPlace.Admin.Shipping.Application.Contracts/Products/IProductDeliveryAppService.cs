using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Shipping.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Shipping.Products
{
    public interface IProductDeliveryAppService : IApplicationService
    {
        Task<ListResultDto<ProductDeliveryDto>> GetListAsync(ProductDeliveryFilterDto input);

        Task<ProductDeliveryDto> GetAsync(Guid id);

        Task UpdateAsync(Guid id, UpdateProductDeliveryDto input);


        Task<ProductDeliveryDto> CreateAsync(Guid id, CreateProductDeliveryDto input);

    }
}
