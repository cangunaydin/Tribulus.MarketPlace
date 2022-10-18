using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Products;
public interface IProductCompositionService : IApplicationService
{
    Task<ProductCompositionDto> GetAsync(Guid id);

    Task<PagedResultDto<ProductCompositionDto>> GetProducts(ProductFilterDto input);
}

