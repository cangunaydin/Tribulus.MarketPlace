
$name = $args[0]
abp new "$name.AbpService" -t module --no-ui -o modules\abp
abp new "$name.AggregateService" -t module --no-ui -o modules\aggregate

Remove-Item -Recurse -Force (Get-ChildItem -r **/*.AbpService.AuthServer)
Remove-Item -Recurse -Force (Get-ChildItem -r **/*.AggregateService.AuthServer)

Remove-Item -Recurse -Force (Get-ChildItem -r **/*.AbpService.MongoDB.Tests)
Remove-Item -Recurse -Force (Get-ChildItem -r **/*.AbpService.MongoDB)
Remove-Item -Recurse -Force (Get-ChildItem -r **/*.AbpService.Host.Shared)
Remove-Item -Recurse -Force (Get-ChildItem -r **/*.AbpService.Installer)

Remove-Item -Recurse -Force (Get-ChildItem -r **/*.AggregateService.MongoDB.Tests)
Remove-Item -Recurse -Force (Get-ChildItem -r **/*.AggregateService.MongoDB)
Remove-Item -Recurse -Force (Get-ChildItem -r **/*.AggregateService.Host.Shared)
Remove-Item -Recurse -Force (Get-ChildItem -r **/*.AggregateService.Installer)


abp add-module Volo.AuditLogging -s "modules\abp\$name.AbpService.sln" --skip-db-migrations
abp add-module Volo.FeatureManagement -s "modules\abp\$name.AbpService.sln" --skip-db-migrations
abp add-module Volo.PermissionManagement -s "modules\abp\$name.AbpService.sln" --skip-db-migrations
abp add-module Volo.SettingManagement -s "modules\abp\$name.AbpService.sln" --skip-db-migrations

abp add-module Volo.Identity -s "modules\abp\$name.AbpService.sln" --skip-db-migrations
abp add-module Volo.OpenIddict -s "modules\abp\$name.AbpService.sln" --skip-db-migrations

abp add-module Volo.TenantManagement -s "modules\abp\$name.AbpService.sln" --skip-db-migrations


