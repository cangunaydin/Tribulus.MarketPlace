using System;
using System.Collections.Generic;
using System.Text;
using Tribulus.MarketPlace.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin;

/* Inherit your application services from this class.
 */
public abstract class AdminAppService : ApplicationService
{
    protected AdminAppService()
    {
        LocalizationResource = typeof(MarketPlaceResource);
    }
}
