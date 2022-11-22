namespace Tribulus.MarketPlace.Marketing;

public static class MarketingDbProperties
{
    public static string DbTablePrefix { get; set; } = "Marketing";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Marketing";
}
