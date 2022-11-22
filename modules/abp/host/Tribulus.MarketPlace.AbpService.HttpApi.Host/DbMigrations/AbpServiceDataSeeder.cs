using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace Tribulus.MarketPlace.AbpService.DbMigrations;

public class AbpServiceDataSeeder : ITransientDependency
{
    private readonly ILogger<AbpServiceDataSeeder> _logger;
    private readonly IIdentityDataSeeder _identityDataSeeder;
    private readonly OpenIddictDataSeeder _openIddictDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    private readonly IPermissionDefinitionManager _permissionDefinitionManager;
    private readonly IPermissionDataSeeder _permissionDataSeeder;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public AbpServiceDataSeeder(
        IIdentityDataSeeder identityDataSeeder,
        OpenIddictDataSeeder openIddictDataSeeder,
        ICurrentTenant currentTenant,
        ILogger<AbpServiceDataSeeder> logger,
        IPermissionDefinitionManager permissionDefinitionManager,
        IPermissionDataSeeder permissionDataSeeder,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _identityDataSeeder = identityDataSeeder;
        _openIddictDataSeeder = openIddictDataSeeder;
        _currentTenant = currentTenant;
        _logger = logger;
        _permissionDefinitionManager = permissionDefinitionManager;
        _permissionDataSeeder = permissionDataSeeder;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task SeedAsync()
    {
        try
        {
            _logger.LogInformation($"Seeding IdentityServer data...");
            await _openIddictDataSeeder.SeedAsync();
            _logger.LogInformation($"Seeding Identity data...");
            await _identityDataSeeder.SeedAsync(
                AbpServiceDbProperties.DefaultAdminEmailAddress,
                AbpServiceDbProperties.DefaultAdminPassword
            );
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task SeedAsync(Guid? tenantId, string adminEmail, string adminPassword)
    {
        try
        {
            using (_currentTenant.Change(tenantId))
            {
                if (tenantId == null)
                {
                    _logger.LogInformation($"Seeding IdentityServer data...");
                    await _openIddictDataSeeder.SeedAsync();
                }

                _logger.LogInformation($"Seeding Identity data...");
                await _identityDataSeeder.SeedAsync(
                    adminEmail,
                    adminPassword,
                    tenantId
                );
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}