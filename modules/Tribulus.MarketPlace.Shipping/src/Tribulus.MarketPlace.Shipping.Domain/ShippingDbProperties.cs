namespace Tribulus.MarketPlace.Shipping;

public static class ShippingDbProperties
{
    public static string DbTablePrefix { get; set; } = "Shipping";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Shipping";
}
