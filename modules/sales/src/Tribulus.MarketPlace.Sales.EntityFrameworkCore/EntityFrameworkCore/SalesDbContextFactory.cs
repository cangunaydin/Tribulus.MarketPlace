using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Tribulus.MarketPlace.Sales.EntityFrameworkCore;


public class SalesDbContextFactory : IDesignTimeDbContextFactory<SalesDbContext>
{
    private readonly string _connectionString;

    /* This constructor is used when you use EF Core tooling (e.g. Update-Database) */
    public SalesDbContextFactory()
    {
        _connectionString = GetConnectionStringFromConfiguration();
    }

    public SalesDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<SalesDbContext>()
            .UseSqlServer(_connectionString, b =>
            {
                b.MigrationsHistoryTable("__SalesService_Migrations");
            });

        return new SalesDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(SalesDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}host{Path.DirectorySeparatorChar}Tribulus.MarketPlace.Admin.Sales.HttpApi.Host"
                )
            )
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
