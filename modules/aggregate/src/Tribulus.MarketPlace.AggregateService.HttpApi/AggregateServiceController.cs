using Tribulus.MarketPlace.AggregateService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.AggregateService;

public abstract class AggregateServiceController : AbpControllerBase
{
    protected AggregateServiceController()
    {
        LocalizationResource = typeof(AggregateServiceResource);
    }
}
