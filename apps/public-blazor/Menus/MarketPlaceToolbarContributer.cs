using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Blazor.Components.Toolbar;
using Tribulus.MarketPlace.Permissions;
using Tribulus.MarketPlace.Sales.Permissions;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Blazor.Menus;

public class MarketPlaceToolbarContributer : IToolbarContributor
{
    public async Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == StandardToolbars.Main)
        {
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

            if (await context.IsGrantedAsync(SalesPermissions.Orders.Create))
                context.Toolbar.Items.Insert(0, new ToolbarItem(typeof(CreateOrderButton)));
        }

    }
}

