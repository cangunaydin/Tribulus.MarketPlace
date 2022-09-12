using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tribulus.MarketPlace.Options
{
    public class MarketPlaceUrlOptions
    {
        private const string ConfigurationName = "AppUrls";
        private const string AccountDefaultValue = "https://localhost:44351";
        private const string AdminDefaultValue = "https://localhost:44312";
        private const string WwwDefaultValue = "https://localhost:44324";

        private const string ApiDefaultValue = "https://localhost:44311";
        private const string ApiInternalDefaultValue = ApiDefaultValue;
        private const string AdminApiDefaultValue = "https://localhost:44354";

        public string Account { get; set; } = AccountDefaultValue;
        public string Www { get; set; } = WwwDefaultValue;
        public string Api { get; set; } = ApiDefaultValue;
        public string ApiInternal { get; set; } = ApiInternalDefaultValue;
        public string Admin { get; set; } = AdminDefaultValue;
        public string AdminApi { get; set; } = AdminApiDefaultValue;

        public static string GetAccountConfigValue(IConfiguration configuration)
        {
            return GetConfigValue(configuration, nameof(Account), AccountDefaultValue);
        }

        public static string GetWwwConfigValue(IConfiguration configuration)
        {
            return GetConfigValue(configuration, nameof(Www), WwwDefaultValue);
        }

        public static string GetApiInternalConfigValue(IConfiguration configuration)
        {
            return GetConfigValue(configuration, nameof(ApiInternal), ApiInternalDefaultValue);
        }

        public static string GetApiConfigValue(IConfiguration configuration)
        {
            return GetConfigValue(configuration, nameof(Api), ApiDefaultValue);
        }

        public static string GetAdminConfigValue(IConfiguration configuration)
        {
            return GetConfigValue(configuration, nameof(Admin), AdminDefaultValue);
        }

        public static string GetAdminApiConfigValue(IConfiguration configuration)
        {
            return GetConfigValue(configuration, nameof(AdminApi), AdminApiDefaultValue);
        }

        private static string GetConfigKey(string appName)
        {
            return $"{ConfigurationName}:{appName}";
        }

        private static string GetConfigValue(
            IConfiguration configuration,
            string appName,
            string defaultValue)
        {
            return configuration[GetConfigKey(appName)] ?? defaultValue;
        }
    }
}
