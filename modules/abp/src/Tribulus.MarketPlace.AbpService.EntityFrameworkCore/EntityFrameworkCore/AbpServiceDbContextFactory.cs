using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tribulus.MarketPlace.AbpService.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands)
 * */
public class AbpServiceDbContextFactory : IDesignTimeDbContextFactory<AbpServiceDbContext>
{
    public AbpServiceDbContext CreateDbContext(string[] args)
    {
        AbpServiceEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<AbpServiceDbContext>()
            .UseSqlServer(GetConnectionStringFromConfiguration(), b =>
            {
                b.MigrationsHistoryTable("__AbpService_Migrations");
            });

        return new AbpServiceDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(AbpServiceDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}host{Path.DirectorySeparatorChar}Tribulus.MarketPlace.AbpService.HttpApi.Host"
                )
            )
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
