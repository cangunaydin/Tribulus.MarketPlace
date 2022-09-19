using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Blazor.Pages;

public partial class Index
{
    private ProductListFilterDto Filter { get; set; }
    private IReadOnlyList<ProductDto> ProductList { get; set; }
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }
    private int PageSize { get; }


    private ProductDto Product { get; set; }
    private Guid EditingProductId { get; set; }
    private UpdateProductDto EditingProduct { get; set; }

    private CreateProductDto CreatingProduct { get; set; }

    private Modal EditProductModal { get; set; }

    private Modal CreateProductModal { get; set; }

    private Validations EditValidationsRef;

    private Validations CreateValidationsRef;

    public Index()
    {
        Filter = new ProductListFilterDto();
        EditingProduct = new UpdateProductDto();
        CreatingProduct = new CreateProductDto();
        PageSize = LimitedResultRequestDto.DefaultMaxResultCount;
        ProductList = new List<ProductDto>();
    }
    protected override async Task OnInitializedAsync()
    {
        await GetProductsAsync();
    }
    private async Task OnKeyPress(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await GetProductsAsync();
        }
    }
    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ProductDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;
        await GetProductsAsync();
        await InvokeAsync(StateHasChanged);
    }
    private async Task GetProductsAsync()
    {
        Filter.SkipCount = PageSize * CurrentPage;
        Filter.MaxResultCount = PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await ProductAppService.GetListAsync(Filter);
        ProductList = result.Items;
        TotalCount = (int)result.TotalCount;
    }


    private async Task OpenEditProductModalAsync(ProductDto input)
    {
        await EditValidationsRef.ClearAll();
        EditingProductId = input.Id;
        Product = await ProductAppService.GetAsync(EditingProductId);


        EditingProduct = ObjectMapper.Map<ProductDto, UpdateProductDto>(Product);
        await EditProductModal.Show();
    }

    private async Task UpdateProductAsync()
    {
        if (await EditValidationsRef.ValidateAll())
        {
            await ProductAppService.UpdateAsync(EditingProductId, EditingProduct);
            await GetProductsAsync();
            await EditProductModal.Hide();
        }
    }

    private async Task OpenCreateProductModalAsync()
    {
        await CreateValidationsRef.ClearAll();
        CreatingProduct = new CreateProductDto();
        await CreateProductModal.Show();
    }
    private async Task CreateProductAsync()
    {
        if (await CreateValidationsRef.ValidateAll())
        {
            await ProductAppService.CreateAsync(CreatingProduct);
            await GetProductsAsync();
            await EditProductModal.Hide();
        }
    }
}
