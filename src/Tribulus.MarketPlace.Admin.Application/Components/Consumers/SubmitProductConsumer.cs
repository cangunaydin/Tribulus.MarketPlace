using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Models;

namespace Tribulus.MarketPlace.Admin.Components.Consumers
{
    public class SubmitProductConsumer : IConsumer<SubmitProduct>
    {
        private readonly IRequestClient<RequestProduct> _client;
        private readonly ILogger<SubmitProductConsumer> _logger;
        public SubmitProductConsumer(IRequestClient<RequestProduct> client, ILogger<SubmitProductConsumer> logger)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Consume(ConsumeContext<SubmitProduct> context)
        {
            var product = context.Message.Product;
            if (product == null)
                throw new InvalidOperationException("There were no products to create!");

            _logger.LogInformation("Submit Product Request ID: {RequestId}", context.RequestId);

            try
            {
                Response<ProductCreateCompleted, ProductCreateFaulted> response = await _client.GetResponse<ProductCreateCompleted, ProductCreateFaulted>(new
                {
                    context.Message.ProductId,
                    Product = product
                });

                if (response.Is(out Response<ProductCreateCompleted> completed))
                {
                    await context.RespondAsync<SubmitProductCompleted>(new
                    {
                        completed.Message.ProductId,
                        completed.Message.Product
                    });
                }
                else if (response.Is(out Response<ProductCreateFaulted> faulted))
                {
                    await context.RespondAsync<SubmitProductFaulted>(new
                    {
                        faulted.Message.ProductId,
                        faulted.Message.Product,
                        faulted.Message.Reason
                    });
                }
            }
            catch (RequestException exception)
            {
                await context.RespondAsync<SubmitProductFaulted>(new
                {
                    context.Message.ProductId,
                    context.Message.Product,
                    Reason = exception.Message,
                });
            }
        }      
    }
}