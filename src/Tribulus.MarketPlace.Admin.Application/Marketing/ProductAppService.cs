using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Permissions;
using Tribulus.MarketPlace.Marketing;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Admin.Marketing
{
    [Authorize]
    public class ProductAppService : MarketPlaceAdminAppService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductAppService(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        [Authorize]
        public async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            var product = new Product(GuidGenerator.Create(), CurrentUser.GetId(), input.Name);
            product.Description = input.Description;
            await _productRepository.InsertAsync(product, true);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [Authorize]
        public async Task<ProductDto> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            return ObjectMapper.Map<Product, ProductDto>(product);
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

        [Authorize]
        public async Task UpdateAsync(Guid id, UpdateProductDto input)
        {
            var product = await _productRepository.GetAsync(id);
            product.UpdateName(input.Name);
            product.Description = input.Description;
            await _productRepository.UpdateAsync(product);
        }
    }
}
