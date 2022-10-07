using System.Collections.Generic;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Marketing;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Blazor.Pages;

public partial class Index
{
    private ProductListFilterDto Filter { get; set; }
    private IReadOnlyList<ProductDto> ProductList { get; set; }
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }
    private int PageSize { get; }

    private readonly IProductAppService _productAppService;

    public Index(IProductAppService productAppService)
    {
        _productAppService = productAppService;
        Filter= new ProductListFilterDto();
        PageSize = LimitedResultRequestDto.DefaultMaxResultCount;
        ProductList = new List<ProductDto>();
    }
    protected override async Task OnInitializedAsync()
    {
        //await GetProductsAsync();
    }
    //private async Task OnKeyPress(KeyboardEventArgs e)
    //{
    //    if (e.Code == "Enter" || e.Code == "NumpadEnter")
    //    {
    //        await GetProductsAsync();
    //    }
    //}
    //private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ProductDto> e)
    //{
    //    CurrentSorting = e.Columns
    //        .Where(c => c.SortDirection != SortDirection.Default)
    //        .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
    //        .JoinAsString(",");
    //    CurrentPage = e.Page - 1;
    //    await GetProductsAsync();
    //    await InvokeAsync(StateHasChanged);
    //}
    //private async Task GetProductsAsync()
    //{
    //    Filter.SkipCount = PageSize * CurrentPage;
    //    Filter.MaxResultCount = PageSize;

    //    var result=await _productAppService.GetListAsync(Filter);
    //    ProductList = result.Items;
    //    TotalCount =(int) result.TotalCount;


    //}
}
