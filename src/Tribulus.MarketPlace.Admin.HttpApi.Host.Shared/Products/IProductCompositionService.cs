﻿using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Volo.Abp.Application.Services;

public interface IProductCompositionService : IApplicationService
{
    Task<ProductViewModelCompositionDto> GetAsync(Guid id);

    Task<ProductListDto> GetProducts(ProductFilterDto input);

    Task<ProductViewModelCompositionDto> CreateProduct(CreateCompleteProductDto input);
}

