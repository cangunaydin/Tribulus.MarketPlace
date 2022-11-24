using Autofac.Core;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Tribulus.MarketPlace.EfCore;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Extensions
{
    public static class MassTransitConfigurationHelper
    {
        public static void Configure(
       ServiceConfigurationContext context, Action<IBusRegistrationConfigurator> configure = null, string efCoreConnectionString = null)
        {


            var configuration = context.Services.GetConfiguration();

            var rabbitMqConnectionString = configuration["RabbitMQ:Connections:Default:HostName"];
            if (string.IsNullOrEmpty(rabbitMqConnectionString))
                throw new Exception("rabbitmq connection string is not provided.");

            var rabbitMqUsername = configuration["RabbitMQ:Connections:Default:UserName"];
            var rabbitMqPassword = configuration["RabbitMQ:Connections:Default:Password"];


            if (!string.IsNullOrEmpty(efCoreConnectionString))
            {
                context.Services.AddDbContext<MarketPlaceSagaDbContext>(builder =>
               builder.UseSqlServer(efCoreConnectionString, m =>
               {
                   m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                   m.MigrationsHistoryTable($"__{nameof(MarketPlaceSagaDbContext)}");
               }));
            }

            context.Services.AddMassTransit(x =>
            {
                x.ApplyMarketPlaceMassTransitConfiguration();

                x.AddDelayedMessageScheduler();

                if (string.IsNullOrEmpty(efCoreConnectionString))
                    x.AddSagaRepository<FutureState>().InMemoryRepository();

                if (!string.IsNullOrEmpty(efCoreConnectionString))
                {
                    x.SetEntityFrameworkSagaRepositoryProvider(r =>
                    {
                        r.ConcurrencyMode = ConcurrencyMode.Pessimistic;
                        r.LockStatementProvider = new SqlServerLockStatementProvider();

                        r.ExistingDbContext<MarketPlaceSagaDbContext>();
                    });

                    x.AddSagaRepository<FutureState>()
                       .EntityFrameworkRepository(r =>
                       {
                           r.ConcurrencyMode = ConcurrencyMode.Pessimistic;
                           r.LockStatementProvider = new SqlServerLockStatementProvider();

                           r.ExistingDbContext<MarketPlaceSagaDbContext>();
                       });
                }

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.AutoStart = true;

                    cfg.UseInstrumentation();

                    cfg.ApplyMarketPlaceBusConfiguration();

                    if (!string.IsNullOrEmpty(rabbitMqUsername) && !string.IsNullOrEmpty(rabbitMqPassword))
                        cfg.Host(rabbitMqConnectionString, h =>
                        {
                            h.Username(rabbitMqUsername);
                            h.Password(rabbitMqPassword);
                        });
                    else
                        cfg.Host(rabbitMqConnectionString);

                    cfg.UseDelayedMessageScheduler();

                    cfg.ConfigureEndpoints(context);
                });
                configure?.Invoke(x);
            });
        }
    }
}
