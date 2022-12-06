using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace Tribulus.MarketPlace.AbpService.DbMigrations;

public class PermissionDataSeeder : ITransientDependency
{
    private readonly ILogger<PermissionDataSeeder> _logger;
    private readonly IPermissionDefinitionManager _permissionDefinitionManager;
    private readonly IPermissionDataSeeder _permissionDataSeeder;
    private readonly ICurrentTenant _currentTenant;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public PermissionDataSeeder(
       ILogger<PermissionDataSeeder> logger,
        IPermissionDefinitionManager permissionDefinitionManager,
        IPermissionDataSeeder permissionDataSeeder,
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _logger = logger;
        _permissionDefinitionManager = permissionDefinitionManager;
        _permissionDataSeeder = permissionDataSeeder;
        _currentTenant = currentTenant;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task SeedAsync(Guid? tenantId = null)
    {
        using (_currentTenant.Change(tenantId))
        {
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true))
            {
                var multiTenancySide = tenantId == null
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant;

                var permissionNames = (await _permissionDefinitionManager
                    .GetPermissionsAsync())
                    .Where(p => p.MultiTenancySide.HasFlag(multiTenancySide))
                    .Where(p => !p.Providers.Any() || p.Providers.Contains(RolePermissionValueProvider.ProviderName))
                    .Select(p => p.Name)
                    .ToArray();

                _logger.LogInformation($"Seeding admin permissions.");
                await _permissionDataSeeder.SeedAsync(
                    RolePermissionValueProvider.ProviderName,
                    "admin",
                    permissionNames,
                    tenantId
                );

                await uow.CompleteAsync();
            }
        }
    }

}
