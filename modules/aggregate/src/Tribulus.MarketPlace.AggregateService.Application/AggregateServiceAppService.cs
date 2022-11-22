using Tribulus.MarketPlace.AggregateService.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.AggregateService;

public abstract class AggregateServiceAppService : ApplicationService
{
    protected AggregateServiceAppService()
    {
        LocalizationResource = typeof(AggregateServiceResource);
        ObjectMapperContext = typeof(AggregateServiceApplicationModule);
    }
}
