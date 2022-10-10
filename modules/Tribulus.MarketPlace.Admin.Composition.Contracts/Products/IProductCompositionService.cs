using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

public interface IProductCompositionService : IApplicationService
{
    Task<ProductCompositionDto> GetAsync(Guid id);

    Task<PagedResultDto<ProductCompositionDto>> GetProducts(ProductFilterDto input);
}

