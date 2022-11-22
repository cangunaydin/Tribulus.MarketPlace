using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Tribulus.MarketPlace.AggregateService.EntityFrameworkCore;

public class ProductServiceDbContextFactory : IDesignTimeDbContextFactory<AggregateServiceDbContext>
{
    private readonly string _connectionString;

    /* This constructor is used when you use EF Core tooling (e.g. Update-Database) */
    public ProductServiceDbContextFactory()
    {
        _connectionString = GetConnectionStringFromConfiguration();
    }

    public AggregateServiceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AggregateServiceDbContext>()
            .UseSqlServer(_connectionString, b =>
            {
                b.MigrationsHistoryTable("__AggregateService_Migrations");
            });

        return new AggregateServiceDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(AggregateServiceDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}host{Path.DirectorySeparatorChar}Tribulus.MarketPlace.AggregateService.HttpApi.Host"
                )
            )
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
