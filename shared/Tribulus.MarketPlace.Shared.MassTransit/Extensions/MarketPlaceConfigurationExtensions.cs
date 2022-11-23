using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tribulus.MarketPlace.Extensions
{
    public static class MarketPlaceConfigurationExtensions
    {
        /// <summary>
        /// Should be using on every UsingRabbitMq configuration
        /// </summary>
        /// <param name="configurator"></param>
        public static void ApplyMarketPlaceBusConfiguration(this IBusFactoryConfigurator configurator)
        {
            var entityNameFormatter = configurator.MessageTopology.EntityNameFormatter;

            configurator.MessageTopology.SetEntityNameFormatter(new MarketPlaceEntityNameFormatter(entityNameFormatter));
        }

        /// <summary>
        /// Should be used on every AddMassTransit configuration
        /// </summary>
        /// <param name="configurator"></param>
        public static void ApplyMarketPlaceMassTransitConfiguration(this IBusRegistrationConfigurator configurator)
        {
            configurator.SetEndpointNameFormatter(new MarketPlaceEndpointNameFormatter());
        }
    }
}
