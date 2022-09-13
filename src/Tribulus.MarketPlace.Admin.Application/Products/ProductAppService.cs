using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Permissions;
using Tribulus.MarketPlace.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Admin.Products
{
    [Authorize]
    public class ProductAppService : MarketPlaceAdminAppService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductAppService(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        [Authorize(MarketPlaceAdminPermissions.Products.Create)]
        public async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            var product = new Product(GuidGenerator.Create(), CurrentUser.GetId(), input.Name, input.Price, input.StockCount);
            product.Description = input.Description;
            await _productRepository.InsertAsync(product, true);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [Authorize(MarketPlaceAdminPermissions.Products.Default)]
        public async Task<ProductDto> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [Authorize(MarketPlaceAdminPermissions.Products.Default)]
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

        [Authorize(MarketPlaceAdminPermissions.Products.Update)]
        public async Task UpdateAsync(Guid id, UpdateProductDto input)
        {
            var product = await _productRepository.GetAsync(id);
            product.UpdateName(input.Name);
            product.UpdatePrice(input.Price);
            product.UpdateStockCount(input.StockCount);
            product.Description = input.Description;
            await _productRepository.UpdateAsync(product);
        }
    }
}
