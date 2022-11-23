using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Tribulus.MarketPlace.Marketing.EntityFrameworkCore;

public class MarketingDbContextFactory : IDesignTimeDbContextFactory<MarketingDbContext>
{
    private readonly string _connectionString;

    /* This constructor is used when you use EF Core tooling (e.g. Update-Database) */
    public MarketingDbContextFactory()
    {
        _connectionString = GetConnectionStringFromConfiguration();
    }

    public MarketingDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MarketingDbContext>()
            .UseSqlServer(_connectionString, b =>
            {
                b.MigrationsHistoryTable("__MarketingService_Migrations");
            });

        return new MarketingDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(MarketingDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}host{Path.DirectorySeparatorChar}Tribulus.MarketPlace.Admin.Marketing.HttpApi.Host"
                )
            )
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
