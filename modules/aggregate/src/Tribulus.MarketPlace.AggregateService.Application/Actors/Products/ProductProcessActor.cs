using Dapr.Actors.Runtime;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Tribulus.MarketPlace.Products;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.AggregateService.Actors.Products
{
    public class ProductProcessActor : Actor, IProductProcessActor, IRemindable
    {
        private readonly ILogger<ProductProcessActor> _logger;

        private const string ProductDetailsStateName = "ProductDetails";
        private const string ProductId = "ProductId";
        private const string ProductStatusStateName = "ProductStatus";

        private const string InventoryConfirmedReminder = "InventoryConfirmed";
        private const string InventoryRejectedReminder = "InventoryRejected";
        private const string MarketingSucceededReminder = "MarketingSucceeded";
        private const string MarketingFailedReminder = "MarketingFailed";
        private const string SalesSucceededReminder = "SalesSucceeded";
        private const string SalesFailedReminder = "SalesFailed";

        private readonly IProductStockAppService _productStockAppService;
        private readonly IProductPriceAppService _productPriceAppService;
        private readonly IProductAppService _productAppService;
        private readonly IOptions<ProductSettings> _settings;
        private readonly IObjectMapper _objectMapper;


        public ProductProcessActor(ActorHost host,
            ILogger<ProductProcessActor> logger,
            IProductStockAppService productStockAppService,
            IProductPriceAppService productPriceAppService,
            IProductAppService productAppService,
            IOptions<ProductSettings> settings,
            IObjectMapper objectMapper
            ) : base(host)
        {
            _logger = logger;
            _productStockAppService = productStockAppService;
            _productPriceAppService = productPriceAppService;
            _productAppService = productAppService;
            _settings = settings;
            _objectMapper = objectMapper;
        }

        public Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
        {
            _logger.LogInformation("Received {Actor}[{ActorId}] reminder: {Reminder}", nameof(ProductProcessActor), ProductId, reminderName);

            switch (reminderName)
            {
                case InventoryConfirmedReminder:
                    return OnInventoryConfirmedSimulatedWorkDone();
                case InventoryRejectedReminder:
                    return OnInventoryRejectedSimulatedWorkDone();
                case MarketingSucceededReminder:
                    return OnMarketingSucceededSimulatedWorkDone();
                case MarketingFailedReminder:
                    OnMarketingFailedSimulatedWorkDone();
                    return OnInventoryRejectedSimulatedWorkDone();
                case SalesSucceededReminder:
                    return OnSalesSucceededSimulatedWorkDone();
                case SalesFailedReminder:
                    OnSalesFailedSimulatedWorkDone();
                    OnMarketingFailedSimulatedWorkDone();
                    return OnInventoryRejectedSimulatedWorkDone();
            }

            _logger.LogError("Unknown actor reminder {ReminderName}", reminderName);
            return Task.CompletedTask;
        }

        public async Task Submit(Guid productId, CreateProductAggregateDto product)
        {
            _logger.LogInformation("Submit state set {ProductDetailsStateName} with {input}", ProductDetailsStateName, product);
            await StateManager.SetStateAsync(ProductDetailsStateName, product);
            await StateManager.SetStateAsync(ProductId, productId);

            await RegisterReminderAsync(
               InventoryConfirmedReminder,
               null,
               TimeSpan.FromSeconds(_settings.Value.GracePeriodTime),
               TimeSpan.FromMilliseconds(-1));

        }


        public async Task OnInventoryConfirmedSimulatedWorkDone()
        {
            try
            {
                var product = await StateManager.GetStateAsync<CreateProductAggregateDto>(ProductDetailsStateName);
                var productId = await StateManager.GetStateAsync<Guid>(ProductId);
                CreateProductStockDto productStockDto = new CreateProductStockDto();
                _objectMapper.Map(product, productStockDto);
                var stockResult = await _productStockAppService.CreateAsync(productId, productStockDto);

                await RegisterReminderAsync(
                    InventoryConfirmedReminder,
                    null,
                    TimeSpan.FromSeconds(_settings.Value.GracePeriodTime),
                    TimeSpan.FromMilliseconds(-1));
            }
            catch (Exception ex)
            {
                await RegisterReminderAsync(
                  InventoryRejectedReminder,
                  null,
                  TimeSpan.FromSeconds(_settings.Value.GracePeriodTime),
                  TimeSpan.FromMilliseconds(-1));
            }
        }

        public async Task OnInventoryRejectedSimulatedWorkDone()
        {
            var productId = await StateManager.GetStateAsync<Guid>(ProductId);
            await _productStockAppService.DeleteAsync(productId);
        }



        public async Task OnMarketingSucceededSimulatedWorkDone()
        {
            try
            {
                var product = await StateManager.GetStateAsync<CreateProductAggregateDto>(ProductDetailsStateName);
                var productId = await StateManager.GetStateAsync<Guid>(ProductId);
                CreateProductDto productDto = new CreateProductDto();
                _objectMapper.Map(product, productDto);
                var productResult = await _productAppService.CreateAsync(productId, productDto);

                await RegisterReminderAsync(
                    MarketingSucceededReminder,
                    null,
                    TimeSpan.FromSeconds(_settings.Value.GracePeriodTime),
                    TimeSpan.FromMilliseconds(-1));
            }
            catch (Exception ex)
            {
                await RegisterReminderAsync(
                  MarketingFailedReminder,
                  null,
                  TimeSpan.FromSeconds(_settings.Value.GracePeriodTime),
                  TimeSpan.FromMilliseconds(-1));
            }
        }

        public async Task OnMarketingFailedSimulatedWorkDone()
        {
            var productId = await StateManager.GetStateAsync<Guid>(ProductId);
            await _productAppService.DeleteAsync(productId);
        }

        public async Task OnSalesSucceededSimulatedWorkDone()
        {
            try
            {
                var product = await StateManager.GetStateAsync<CreateProductAggregateDto>(ProductDetailsStateName);
                var productId = await StateManager.GetStateAsync<Guid>(ProductId);
                CreateProductPriceDto productPriceDto = new CreateProductPriceDto();
                _objectMapper.Map(product, productPriceDto);
                var productResult = await _productPriceAppService.CreateAsync(productId, productPriceDto);

                await RegisterReminderAsync(
                    SalesSucceededReminder,
                    null,
                    TimeSpan.FromSeconds(_settings.Value.GracePeriodTime),
                    TimeSpan.FromMilliseconds(-1));
            }
            catch (Exception ex)
            {
                await RegisterReminderAsync(
                  SalesFailedReminder,
                  null,
                  TimeSpan.FromSeconds(_settings.Value.GracePeriodTime),
                  TimeSpan.FromMilliseconds(-1));
            }
        }

        public async Task OnSalesFailedSimulatedWorkDone()
        {
            var productId = await StateManager.GetStateAsync<Guid>(ProductId);
            await _productPriceAppService.DeleteAsync(productId);
        }

    }
}
