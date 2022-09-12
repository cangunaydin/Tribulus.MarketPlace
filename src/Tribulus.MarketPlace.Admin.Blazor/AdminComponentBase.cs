
using Tribulus.MarketPlace.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Tribulus.MarketPlace.Admin.Blazor;

public abstract class AdminComponentBase : AbpComponentBase
{
    protected AdminComponentBase()
    {
        LocalizationResource = typeof(MarketPlaceResource);
    }
}
