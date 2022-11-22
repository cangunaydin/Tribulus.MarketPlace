
using Tribulus.MarketPlace.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Tribulus.MarketPlace.Admin.Blazor;

public abstract class MarketPlaceAdminComponentBase : AbpComponentBase
{
    protected MarketPlaceAdminComponentBase()
    {
        LocalizationResource = typeof(MarketPlaceResource);
    }
}
