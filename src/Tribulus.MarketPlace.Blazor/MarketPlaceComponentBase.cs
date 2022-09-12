using Tribulus.MarketPlace.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Tribulus.MarketPlace.Blazor;

public abstract class MarketPlaceComponentBase : AbpComponentBase
{
    protected MarketPlaceComponentBase()
    {
        LocalizationResource = typeof(MarketPlaceResource);
    }
}
