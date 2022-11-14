using System;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace.Marketing;

public class MarketingTestData : ISingletonDependency
{

    public Guid UserJohnId { get; } = Guid.NewGuid();
    public string UserJohnUserName { get; } = "john";

    public Guid ProductIphone13Id { get; } = Guid.NewGuid();
    public string ProductIphone13Name { get; } = "Iphone 13";


    public Guid ProductIphone13ProId { get; } = Guid.NewGuid();
    public string ProductIphone13ProName { get; } = "Iphone 13 Pro";
    public string ProductIphone13ProDescription { get; } = "Pro And Beyond";

    public Guid ProductIphone14Id { get; } = Guid.NewGuid();
    public string ProductIphone14Name { get; } = "Iphone 14";
    public string ProductIphone14Description { get; } = "Iphone 14, available starting 9.16";

}