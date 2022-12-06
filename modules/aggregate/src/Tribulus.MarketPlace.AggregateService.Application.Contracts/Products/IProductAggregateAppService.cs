using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.AggregateService.Products;
public interface IProductAggregateAppService : IApplicationService
{
    Task<ProductAggregateDto> GetAsync(Guid id);

    Task<PagedResultDto<ProductAggregateDto>> GetProducts(ProductAggregateFilterDto input);

    Task<ProductAggregateDto> CreateAsync(CreateProductAggregateDto input);
}

