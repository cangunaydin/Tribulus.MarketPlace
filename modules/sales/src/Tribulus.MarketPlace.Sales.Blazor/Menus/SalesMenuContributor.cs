using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Tribulus.MarketPlace.Sales.Blazor.Menus;

public class SalesMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(SalesMenus.Prefix, displayName: "Sales", "/Sales", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
