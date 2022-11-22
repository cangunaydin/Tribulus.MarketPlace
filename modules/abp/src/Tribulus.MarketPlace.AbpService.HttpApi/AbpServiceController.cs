using Tribulus.MarketPlace.AbpService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.AbpService;

public abstract class AbpServiceController : AbpControllerBase
{
    protected AbpServiceController()
    {
        LocalizationResource = typeof(AbpServiceResource);
    }
}
