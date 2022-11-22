using Tribulus.MarketPlace.AbpService.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.AbpService;

public abstract class AbpServiceAppService : ApplicationService
{
    protected AbpServiceAppService()
    {
        LocalizationResource = typeof(AbpServiceResource);
        ObjectMapperContext = typeof(AbpServiceApplicationModule);
    }
}
