using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tribulus.MarketPlace.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class MarketPlaceDbContextFactory : IDesignTimeDbContextFactory<MarketPlaceDbContext>
{
    public MarketPlaceDbContext CreateDbContext(string[] args)
    {
        MarketPlaceEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<MarketPlaceDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new MarketPlaceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Tribulus.MarketPlace.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
