using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Blazor.Menus;

public class MarketPlaceMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public MarketPlaceMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<MarketPlaceResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                MarketPlaceMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
            )
        );

        return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();
        var l = context.GetLocalizer<MarketPlaceResource>();
        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());

        context.Menu.AddItem(new ApplicationMenuItem(
             "Account.MyOrders",
             l["MyOrders"],
             "/account/myorders",
             icon: "fas fa-shopping-cart",
             order: 1000,
             null).RequireAuthenticated());

        return Task.CompletedTask;
    }
}
