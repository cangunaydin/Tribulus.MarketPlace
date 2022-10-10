﻿using System.Collections.Generic;
using Volo.Abp.Auditing;
using Volo.Abp.Authorization;
using Volo.Abp.Domain;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Http;
using Volo.Abp.Http.Modeling;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Security;
using Volo.Abp.Settings;
using Volo.Abp.Uow;
using Volo.Abp.Validation;

namespace Tribulus.MarketPlace.Composition
{
    [DependsOn(typeof(AbpDddDomainModule),
    typeof(AbpSecurityModule),
    typeof(AbpObjectMappingModule),
    typeof(AbpValidationModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpHttpAbstractionsModule),
    typeof(AbpSettingsModule),
    typeof(AbpFeaturesModule),
    typeof(AbpGlobalFeaturesModule))]
    public class MarketPlaceCompositionModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpApiDescriptionModelOptions>(options =>
            {
                options.IgnoredInterfaces.AddIfNotContains(typeof(IUnitOfWorkEnabled));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IAuditingEnabled));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IValidationEnabled));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IGlobalFeatureCheckingEnabled));
            });
        }
        
    }
}
