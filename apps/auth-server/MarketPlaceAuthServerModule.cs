using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Tribulus.MarketPlace.AbpService.EntityFrameworkCore;
using Tribulus.MarketPlace.Localization;
using Tribulus.MarketPlace.Shared.Hosting.AspNetCore;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.Auditing;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Emailing;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.OpenIddict;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace Tribulus.MarketPlace;

[DependsOn(
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpEventBusRabbitMqModule),
    typeof(AbpBackgroundJobsRabbitMqModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),

    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpAccountHttpApiModule),

    typeof(AbpServiceEntityFrameworkCoreModule),
    typeof(MarketPlaceSharedHostingAspNetCoreModule),
    typeof(MarketPlaceSharedLocalizationModule)
)]
public class MarketPlaceAuthServerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("AccountService");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });

        if (!hostingEnvironment.IsDevelopment())
        {
            PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
            {
                options.AddDevelopmentEncryptionAndSigningCertificate = false;
            });

            PreConfigure<OpenIddictServerBuilder>(builder =>
            {
                builder.AddSigningCertificate(GetSigningCertificate(hostingEnvironment, configuration));
                builder.AddEncryptionCertificate(GetSigningCertificate(hostingEnvironment, configuration));
            });
        }
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //You can disable this setting in production to avoid any potential security risks.
        Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        ConfigureBundles();
        ConfigureSwagger(context, configuration);
        ConfigureSameSiteCookiePolicy(context);
        ConfigureExternalProviders(context);

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = false;
        });

        Configure<AbpAuditingOptions>(options =>
        {
            options.ApplicationName = "AuthServer";
        });

        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
        });

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "MarketPlace:";
        });

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("MarketPlace");
        //var redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("redis"));
        var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
        dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "MarketPlace-Protection-Keys");

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.Trim().RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

#if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<MarketPlaceSharedLocalizationModule>(Path.Combine(
                    hostingEnvironment.ContentRootPath,
                    $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}shared{Path.DirectorySeparatorChar}Tribulus.MarketPlace.Shared.Localization"));
            });
        }

      
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseAbpSecurityHeaders();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseCookiePolicy();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();
        //app.UseMultiTenancy();
        app.UseAbpSerilogEnrichers();
        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Account Service API");
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
        });
        app.UseAuditing();
        app.UseConfiguredEndpoints();
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureExternalProviders(ServiceConfigurationContext context)
    {
        //context.Services.AddAuthentication()
        //    .AddGoogle(GoogleDefaults.AuthenticationScheme, _ => { })
        //    .WithDynamicOptions<GoogleOptions, GoogleHandler>(
        //        GoogleDefaults.AuthenticationScheme,
        //        options =>
        //        {
        //            options.WithProperty(x => x.ClientId);
        //            options.WithProperty(x => x.ClientSecret, isSecret: true);
        //        }
        //    )
        //    .AddMicrosoftAccount(MicrosoftAccountDefaults.AuthenticationScheme, options =>
        //    {
        //        //Personal Microsoft accounts as an example.
        //        options.AuthorizationEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize";
        //        options.TokenEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/token";
        //    })
        //    .WithDynamicOptions<MicrosoftAccountOptions, MicrosoftAccountHandler>(
        //        MicrosoftAccountDefaults.AuthenticationScheme,
        //        options =>
        //        {
        //            options.WithProperty(x => x.ClientId);
        //            options.WithProperty(x => x.ClientSecret, isSecret: true);
        //        }
        //    )
        //    .AddTwitter(TwitterDefaults.AuthenticationScheme, options => options.RetrieveUserDetails = true)
        //    .WithDynamicOptions<TwitterOptions, TwitterHandler>(
        //        TwitterDefaults.AuthenticationScheme,
        //        options =>
        //        {
        //            options.WithProperty(x => x.ConsumerKey);
        //            options.WithProperty(x => x.ConsumerSecret, isSecret: true);
        //        }
        //    );
    }

    private X509Certificate2 GetSigningCertificate(IWebHostEnvironment hostingEnv, IConfiguration configuration)
    {
        var fileName = "authserver.pfx";
        var passPhrase = "2D7AA457-5D33-48D6-936F-C48E5EF468ED";
        var file = Path.Combine(hostingEnv.ContentRootPath, fileName);

        if (!File.Exists(file))
        {
            throw new FileNotFoundException($"Signing Certificate couldn't found: {file}");
        }

        return new X509Certificate2(file, passPhrase);
    }

    private void ConfigureSwagger(ServiceConfigurationContext context, IConfiguration configuration)
    {
        SwaggerConfigurationHelper.ConfigureWithAuth(
            context: context,
            authority: configuration["AuthServer:Authority"],
            scopes: new Dictionary<string, string> {
                /* Requested scopes for authorization code request and descriptions for swagger UI only */
                { "AccountService", "Account Service API" }
            },
            apiTitle: "Account Service API"
        );
    }

    private void ConfigureSameSiteCookiePolicy(ServiceConfigurationContext context)
    {
        context.Services.AddSameSiteCookiePolicy();
    }
}
