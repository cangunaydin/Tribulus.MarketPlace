using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Inventory;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tribulus.MarketPlace.EntityFrameworkCore
{
    public static class MarketPlaceDbContextModelCreatingExtensions
    {
        public static void ConfigureMarketPlace(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

           
           

        }
    }
}
