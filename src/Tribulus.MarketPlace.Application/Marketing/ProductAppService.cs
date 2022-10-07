using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Marketing
{
    [Authorize]
    public class ProductAppService : MarketPlaceAppService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductAppService(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(ProductListFilterDto input)
        {
            var query = await _productRepository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Name), o => o.Name.Contains(input.Name));
            query = query.PageBy(input);

            var productsCount = await AsyncExecuter.CountAsync(query);
            var products = await AsyncExecuter.ToListAsync(query);
            var productsDto = ObjectMapper.Map<List<Product>, List<ProductDto>>(products);

            return new PagedResultDto<ProductDto>(productsCount, productsDto);
        }
    }
}
