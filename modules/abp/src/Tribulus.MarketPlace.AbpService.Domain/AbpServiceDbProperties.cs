namespace Tribulus.MarketPlace.AbpService;

public static class AbpServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "AbpService";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "AbpService";

    public const string DefaultAdminEmailAddress = "admin@doohclick.com";
    public const string DefaultAdminPassword = "1q2w3E*";
}
