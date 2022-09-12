using Volo.Abp.Settings;

namespace Tribulus.MarketPlace.Settings;

public class MarketPlaceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MarketPlaceSettings.MySetting1));
    }
}
