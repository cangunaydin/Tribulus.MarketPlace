using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Marketing.Permissions;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Admin.Marketing.Products
{
    [Authorize(MarketingPermissions.Products.Default)]
    public class ProductAppService : AdminMarketingAppService, IProductAppService
    {
        private readonly IProductRepository _productRepository;

        public ProductAppService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [Authorize(MarketingPermissions.Products.Create)]
        public async Task<ProductDto> CreateAsync(Guid id,CreateProductDto input)
        {
            var product = new Product(id, CurrentUser.GetId(), input.Name);
            product.Description = input.Description;
            await _productRepository.InsertAsync(product, true);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

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

        [Authorize(MarketingPermissions.Products.Update)]
        public async Task UpdateAsync(Guid id, UpdateProductDto input)
        {
            var product = await _productRepository.GetAsync(id);
            product.UpdateName(input.Name);
            product.Description = input.Description;
            await _productRepository.UpdateAsync(product);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
