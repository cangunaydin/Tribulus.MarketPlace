using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Products;
using Volo.Abp.Application.Dtos;
using Blazorise;
using Blazorise.DataGrid;
using Tribulus.MarketPlace.Orders;
using Microsoft.AspNetCore.Components;
using Blazorise.Snackbar;
using static Tribulus.MarketPlace.Permissions.MarketPlacePermissions;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Blazor.Pages;
public partial class Index
{
    private ProductListFilterDto Filter { get; set; }
    private OrderFilterDto OrderFilter { get; set; }
    private IReadOnlyList<ProductDto> ProductList { get; set; }
    private IReadOnlyList<OrderDto> OrderList { get; set; }
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }

    private bool TableHidden = true;

    private bool HasOrderItems = false;
    private int TotalCount { get; set; }
    private int PageSize { get; }

    private readonly IProductAppService _productAppService;

    IOrderAppService _orderAppService;
    private CreateOrderDto CreatingOrder { get; set; }
    private Modal CreateOrderModal { get; set; }

    private ProductDto SelectedProduct { get; set; }
    private CreateOrderItemDto AddingOrderItem { get; set; }
    private Modal AddorderItemModal { get; set; }

    private OrderDto OrderDto { get; set; }
    private Modal EditOrderModal { get; set; }
    private Modal DisplayOrderDetailsModal { get; set; }

    private Validations CreateValidationsRef;

    private bool Hasorder = false;

    private Snackbar OrderSnackbar;

    private Snackbar OrderItemSnackbar;

    private Snackbar OrderPlacedSnackbar;

    private Snackbar OrderItemRemovedSnackbar;

    private Snackbar OrderDeletedSnackbar;


    //private Snackbar Snackbar;
    public Index(IProductAppService productAppService, IOrderAppService orderAppService)
    {
        _orderAppService = orderAppService;
        _productAppService = productAppService;
        OrderFilter = new OrderFilterDto();
        Filter = new ProductListFilterDto();
        AddingOrderItem = new CreateOrderItemDto();
        CreatingOrder = new CreateOrderDto();
        OrderDto = new OrderDto();
        SelectedProduct = new ProductDto();
        PageSize = LimitedResultRequestDto.DefaultMaxResultCount;
        ProductList = new List<ProductDto>();
    }
    protected override async Task OnInitializedAsync()
    {
        await GetProductsAsync();
        await GetActiveStateOrder();
    }

    private async Task GetActiveStateOrder()
    {
        OrderDto = await _orderAppService.GetCurrentOrderAsync();

        if (CurrentUser.IsAuthenticated && OrderDto != null)
        {
            Hasorder = true;

            if(OrderDto.OrderItems.Count > 0)
            {
                HasOrderItems = true;
            }
            CreatingOrder.Name = OrderDto.Name;
        }
    }

    private async Task OnKeyPress(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await GetProductsAsync();
        }
    }


    private async Task OnOrderKeyPress(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await GetOrdersAsync();
        }
    }
    private async Task OnOrderDataGridReadAsync(DataGridReadDataEventArgs<OrderDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;
        await GetOrdersAsync();
        await InvokeAsync(StateHasChanged);
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

    private void OnQuantityChanged(int i)
    {
        AddingOrderItem.Quantity = i;
        AddingOrderItem.Price = i * SelectedProduct.Price;
    }

    private async Task OpenCreateOrderModalAsync()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            await CreateOrderModal.Hide();
            UriHelper.NavigateTo("authentication/login");
            return;
        }
        await CreateValidationsRef.ClearAll();
        CreatingOrder = new CreateOrderDto();
        await CreateOrderModal.Show();
    }


    private async Task OpenDisplayOrderModalAsync()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            await DisplayOrderDetailsModal.Hide();
            UriHelper.NavigateTo("authentication/login");
            return;
        }
        await CreateValidationsRef.ClearAll();
        await GetOrdersAsync();
        await DisplayOrderDetailsModal.Show();
    }



    private async Task OpenAddOrderItemModalAsync()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            await CreateOrderModal.Hide();
            UriHelper.NavigateTo("authentication/login");
            return;
        }
        await CreateValidationsRef.ClearAll();
        AddingOrderItem = new CreateOrderItemDto();
        await AddorderItemModal.Show();
    }

    private async Task CreateOrderAsync()
    {
        if (await CreateValidationsRef.ValidateAll())
        {
            OrderDto = await _orderAppService.CreateAsync(CreatingOrder);
            Hasorder = true;
            await CreateOrderModal.Hide();
            await OrderSnackbar.Show();
        }
    }

    private void DisplayTable()
    {
        TableHidden = false;
    }

    private async Task DeleteOrderItem(Guid OrderItemid)
    {
        await _orderAppService.DeleteOrderItem(OrderItemid, OrderDto.Id);
        await DisplayOrderDetailsModal.Hide();
        await OrderItemRemovedSnackbar.Show();
        await DisplayOrderDetailsModal.Hide();
        if (OrderDto.OrderItems.Count < 1)
        {
            HasOrderItems = false;
        }

    }

    private async Task DeleteOrder()
    {
        await _orderAppService.DeleteOrder(OrderDto.Id);
        await DisplayOrderDetailsModal.Hide();
        await OrderDeletedSnackbar.Show();
        await DisplayOrderDetailsModal.Hide();
        Hasorder = false;

    }

    private async Task AddOrderItemAsync()
    {
        if (await CreateValidationsRef.ValidateAll())
        {
            AddingOrderItem.ProductId = SelectedProduct.Id;
            await _orderAppService.CreateOrderItemAsync(OrderDto.Id, AddingOrderItem);
            HasOrderItems = true;
            await AddorderItemModal.Hide();
            await OrderItemSnackbar.Show();

        }
    }

    private async Task PlaceOrder()
    {
        await _orderAppService.PlaceOrder(OrderDto.Id);
        await DisplayOrderDetailsModal.Hide();
        Hasorder = false;
        await OrderPlacedSnackbar.Show();
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

    private async Task GetOrdersAsync()
    {
        OrderFilter.SkipCount = PageSize * CurrentPage;
        OrderFilter.MaxResultCount = PageSize;
        OrderFilter.Sorting = CurrentSorting;
        var result = await _orderAppService.GetOrdersAsync(OrderFilter);
        OrderList = result.Items;
        TotalCount = (int)result.TotalCount;
    }


}