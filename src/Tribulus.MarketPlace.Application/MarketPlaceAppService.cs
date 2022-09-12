using System;
using System.Collections.Generic;
using System.Text;
using Tribulus.MarketPlace.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace;

/* Inherit your application services from this class.
 */
public abstract class MarketPlaceAppService : ApplicationService
{
    protected MarketPlaceAppService()
    {
        LocalizationResource = typeof(MarketPlaceResource);
    }
}
