using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tribulus.MarketPlace.Shipping;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class ShippingInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ShippingInstallerModule>();
        });
    }
}
