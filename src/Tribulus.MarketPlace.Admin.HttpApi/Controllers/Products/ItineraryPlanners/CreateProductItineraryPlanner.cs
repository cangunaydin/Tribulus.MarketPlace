﻿using MassTransit.Courier;
using MassTransit.Futures;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Controllers.Products.Commands;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Tribulus.MarketPlace.Extensions;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.ItineraryPlanners;

public class CreateProductItineraryPlanner : IItineraryPlanner<CreateProduct>,ITransientDependency
{
    private readonly MarketPlaceEndpointNameFormatter _endpointNameformatter;
    public CreateProductItineraryPlanner(MarketPlaceEndpointNameFormatter endpointNameformatter)
    {
        _endpointNameformatter = endpointNameformatter;
    }
    public void AddActivity(CreateProduct value, ItineraryBuilder builder, Type activity)
    {
        var address = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(activity)}");
        builder.AddActivity(activity.Name, address, value);
    }

    public Task PlanItinerary(FutureConsumeContext<CreateProduct> context, ItineraryBuilder builder)
    {
        var product = context.Message;
        var createProductAddress = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(typeof(ICreateProductActivity))}");

        builder.AddVariable(nameof(product.ProductId), product.ProductId);
        builder.AddVariable(nameof(product.UserId), product.UserId);
        builder.AddActivity(_endpointNameformatter.GetActivityName(typeof(ICreateProductActivity)), createProductAddress, new
        {
            Name = product.Name,
            Description = product.Description
        });

        var createProductPriceAddress = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(typeof(ICreateProductPriceActivity))}");

        builder.AddActivity(_endpointNameformatter.GetActivityName(typeof(ICreateProductPriceActivity)), createProductPriceAddress, new
        {
            Price = product.Price
        });

        var createProductStockAddress = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(typeof(ICreateProductStockActivity))}");

        builder.AddActivity(_endpointNameformatter.GetActivityName(typeof(ICreateProductStockActivity)), createProductStockAddress, new
        {
            StockCount = product.StockCount
        });
        return Task.CompletedTask;
    }

}