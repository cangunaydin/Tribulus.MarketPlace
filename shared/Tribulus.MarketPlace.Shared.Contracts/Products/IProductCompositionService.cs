using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Products;
public interface IProductCompositionService : IApplicationService
{
    Task<ProductCompositionDto> GetAsync(Guid id);

    Task<PagedResultDto<ProductCompositionDto>> GetProducts(ProductFilterDto input);

    Task<ProductCompositionDto> CreateAsync(CreateProductCompositionDto input);
}

