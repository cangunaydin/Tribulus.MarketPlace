using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Products;
public interface IProductAggregateAppService : IApplicationService
{
    Task<ProductAggregateDto> GetAsync(Guid id);

    Task<PagedResultDto<ProductAggregateDto>> GetProducts(ProductFilterDto input);

    Task<ProductAggregateDto> CreateAsync(CreateProductAggregateDto input);
}

