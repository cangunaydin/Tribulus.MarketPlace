
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Products;
using Volo.Abp.Application.Dtos;
using Blazorise;
using Blazorise.DataGrid;
using Tribulus.MarketPlace.Orders;
using Blazorise.Snackbar;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Blazor.Pages;

public partial class Index
{
    private readonly IProductAppService _productAppService;

    private ProductListFilterDto Filter { get; set; }
    private IReadOnlyList<ProductDto> ProductList { get; set; }
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }
    private int PageSize { get; }

    private ProductDto SelectedProduct;
    private CreateOrderDto CreatingOrder { get; set; }
    private CreateOrderItemDto CreatingOrderItem { get; set; }
    private OrderDto Order { get; set; }
    private Modal CreateOrderModal { get; set; }
    private Modal CreateOrderItemModal { get; set; }
    private Modal EditOrderModal { get; set; }
    private Modal OrderDetailModal { get; set; }

    private Validations CreateOrderValidationsRef;
    private Validations CreateOrderItemValidationsRef;

    private Snackbar OrderSnackbar;
    private Snackbar OrderItemSnackbar;
    private Snackbar OrderItemDeleteSnackbar;
    private Snackbar PlaceOrderSnackbar;
    

    public Index(IProductAppService productAppService)
    {
        _productAppService = productAppService;
        Filter = new ProductListFilterDto();
        PageSize = LimitedResultRequestDto.DefaultMaxResultCount;
        ProductList = new List<ProductDto>();

        CreatingOrder = new CreateOrderDto();
        CreatingOrderItem = new CreateOrderItemDto();
        SelectedProduct = new ProductDto();
    }


    protected override async Task OnInitializedAsync()
    {
        await GetActiveStateOrder();
        await GetProductsAsync();
    }

    private async Task GetActiveStateOrder()
    {
        if (CurrentUser.IsAuthenticated)
        {
            Order = await OrderAppService.GetOrderByUserIdAsync(CurrentUser.GetId());
            if (Order != null)
                CreatingOrder.Name = Order.Name;
        }
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

    private async Task OpenCreateOrderModalAsync()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            await CreateOrderModal.Hide();
            UriHelper.NavigateTo("authentication/login");
            return;
        }
        await CreateOrderValidationsRef.ClearAll();
        CreatingOrder = new CreateOrderDto();
        await CreateOrderModal.Show();


    }

    private async Task OpenAddToOrderModalAsync()
    {
        await CreateOrderItemValidationsRef.ClearAll();
        CreatingOrderItem = new CreateOrderItemDto();
        CreatingOrderItem.Quantity = 1;
        await CreateOrderItemModal.Show();
    }

    private async Task OpenOrderDetailModalAsync()
    {
        await OrderDetailModal.Show();
    }

    private async Task CreateOrderAsync()
    {
        if (await CreateOrderValidationsRef.ValidateAll())
        {
            Order = await OrderAppService.CreateAsync(CreatingOrder);
            //await GetProductsAsync();
            await CreateOrderModal.Hide();
            await OrderSnackbar.Show();
        }
    }

    private async Task CreateOrderItemAsync()
    {
        if (await CreateOrderItemValidationsRef.ValidateAll())
        {
            CreatingOrderItem.OrderId = Order.Id;
            CreatingOrderItem.ProductId = SelectedProduct.Id;
            CreatingOrderItem.Price = CreatingOrderItem.Quantity * SelectedProduct.Price;
            var orderItems = await OrderAppService.CreateOrderItemAsync(Order.Id, CreatingOrderItem);
            Order.OrderItems.Add(orderItems);
            await CreateOrderItemModal.Hide();
            await OrderItemSnackbar.Show();
        }
    }

    private async Task PlaceOrderAsync()
    {
        await OrderAppService.PlaceOrder(Order.Id);
        await OrderDetailModal.Hide();
        await PlaceOrderSnackbar.Show();
        CreatingOrder = new CreateOrderDto();
        CreatingOrderItem = new CreateOrderItemDto();
        Order = new OrderDto();
        Order.OrderItems = new();
    }


    private async Task DeleteItems(OrderItemDto orderItem)
    {
        await OrderAppService.DeleteOrderItem(orderItem.Id, Order.Id);
        Order.OrderItems.Remove(orderItem);
        await OrderItemDeleteSnackbar.Show();
    }

    private async Task GetProductsAsync()
    {
        Filter.SkipCount = PageSize * CurrentPage;
        Filter.MaxResultCount = PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await _productAppService.GetListAsync(Filter);
        ProductList = result.Items;
        TotalCount = (int)result.TotalCount;


    }
}
