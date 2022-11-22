namespace Tribulus.MarketPlace.AggregateService;

public static class AggregateServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "AggregateService";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "AggregateService";
}
