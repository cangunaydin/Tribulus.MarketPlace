using MassTransit;
using MassTransit.Components;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Components.Consumers;
using Tribulus.MarketPlace.Admin.Constants;
using Tribulus.MarketPlace.Admin.Inventory.Components.Activities;
using Tribulus.MarketPlace.Admin.Marketing.Components.Activities;
using Tribulus.MarketPlace.Admin.Models;
using Tribulus.MarketPlace.Admin.Products.StateMachine;
using Tribulus.MarketPlace.Admin.StateMachine;
using Xunit;

namespace Tribulus.MarketPlace.Admin.Consumers
{
    public class SubmitProductConsumer_Tests : MarketPlaceAdminApplicationTestBase
    {
        public SubmitProductConsumer_Tests()
        {
        }


        [Fact]
        public async Task Should_start_and_stop_the_test_harness()
        {
            await using var serviceProvider = new ServiceCollection()
                   .AddMassTransitTestHarness(cfg =>
                   {
                       cfg.AddConsumer<SubmitProductConsumer>();
                   })
                   .BuildServiceProvider(true);

            var harness = serviceProvider.GetRequiredService<ITestHarness>();
            await harness.Start();

            await harness.Stop();
        }


        //[Fact]
        //public async Task Should_Response_With_Acceptance_If_Ok()
        //{
        //    await using var serviceProvider = new ServiceCollection()
        //            .AddMassTransitTestHarness(cfg =>
        //            {
        //                cfg.AddConsumer<SubmitProductConsumer>();
        //                cfg.AddSagaStateMachine<ProductStateMachine, ProductState>();
        //                cfg.AddRequestClient<SubmitProduct>();
        //            })
        //            .BuildServiceProvider(true);

        //    var harness = serviceProvider.GetRequiredService<ITestHarness>();
        //    //var services = new ServiceCollection();
        //    //TODO: add all dependencies
        //    //var serviceProvider = services.BuildServiceProvider();

        //    ////because DI used through constructor of SubmitProductConsumer
        //    //var consumerHarness = harness.Consumer(() =>
        //    //{
        //    //    var consumer = serviceProvider.GetRequiredService<SubmitProductConsumer>();
        //    //    return consumer;
        //    //});

        //    await harness.Start();
        //    try
        //    {
        //        var client = harness.GetRequestClient<SubmitProduct>();

        //        var productId = Guid.NewGuid();
        //        var model = new
        //        {
        //            ProductId = productId,
        //            Product = new Product()
        //            {
        //                Name = "iPhone 15 pro max",
        //                Description = "iPhone 15 pro max is the new model of Apple mobile devices",
        //                Price = 1599,
        //                StockCount = 0
        //            }
        //        };
        //        model.Product.ProductId = productId;

        //        await client.GetResponse<SubmitProductCompleted, SubmitProductFaulted>(model);

        //        var consumerHarness = harness.GetConsumerHarness<SubmitProductConsumer>();
        //        Assert.True(consumerHarness.Consumed.Select<SubmitProduct>().Any());
        //        Assert.True(await consumerHarness.Consumed.Any<SubmitProduct>());


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        await harness.Stop();
        //        await serviceProvider.DisposeAsync();
        //    }
        //}

        //[Fact]
        //public async Task Should_test_the_consumer()
        //{
        //    var harness = new InMemoryTestHarness();
        //    var services = new ServiceCollection()
        //        .AddMassTransitTestHarness(cfg =>
        //        {
        //            cfg.AddConsumer<SubmitProductConsumer>();                   
        //        });
        //    //TODO: add all dependencies
        //    var serviceProvider = services.BuildServiceProvider();

        //    ////because DI used through constructor of SubmitProductConsumer
        //    var consumerHarness = harness.Consumer(() =>
        //    {
        //        var consumer = serviceProvider.GetRequiredService<SubmitProductConsumer>();
        //        return consumer;
        //    });


        //    await harness.Start();
        //    try
        //    {
        //        var productId = Guid.NewGuid();
        //        var model = new
        //        {
        //            ProductId = productId,
        //            Product = new Product()
        //            {
        //                Name = "iPhone 15 pro max",
        //                Description = "iPhone 15 pro max is the new model of Apple mobile devices",
        //                Price = 1599,
        //                StockCount = 0
        //            }
        //        };
        //        model.Product.ProductId = productId;
        //        await harness.InputQueueSendEndpoint.Send<SubmitProduct>(model);

        //        // did the endpoint consume the message
        //        var hasEndpointConsumedMsg = harness.Consumed.Select<SubmitProduct>().Any().ShouldBeTrue();
        //        Assert.True(hasEndpointConsumedMsg);

        //        //// did the actual consumer consume the message
        //        //var hasActualConsumerConsumedMsg = consumerHarness.Consumed.Select<SubmitProduct>().Any();
        //        //Assert.True(hasActualConsumerConsumedMsg);

        //        //// did the actual consumer throws exception
        //        //var hasFaultMsg = harness.Published.Select<Fault<SubmitProduct>>().Any();
        //        //Assert.True(hasFaultMsg);
        //    }
        //    finally
        //    {
        //        await harness.Stop();
        //    }
        //}

    }
}
