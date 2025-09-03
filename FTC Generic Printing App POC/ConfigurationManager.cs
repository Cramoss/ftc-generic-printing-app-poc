using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTC_Generic_Printing_App_POC
{
    public static class ConfigurationManager
    {
        public static void SaveConfiguration(ConfigurationData config)
        {
            try
            {
                AppLogger.LogInfo("Saving configuration...");
                var configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                configFile.AppSettings.Settings.Remove("IdTotem");
                configFile.AppSettings.Settings.Remove("Country");
                configFile.AppSettings.Settings.Remove("Business");
                configFile.AppSettings.Settings.Remove("Store");
                configFile.AppSettings.Settings.Remove("StoreId");

                configFile.AppSettings.Settings.Add("IdTotem", config.IdTotem);
                configFile.AppSettings.Settings.Add("Country", config.Country);
                configFile.AppSettings.Settings.Add("Business", config.Business);
                configFile.AppSettings.Settings.Add("Store", config.Store);
                configFile.AppSettings.Settings.Add("StoreId", config.StoreId);

                configFile.Save(ConfigurationSaveMode.Modified);
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

                AppLogger.LogInfo($"Configuration saved successfully. " +
                    $"IdTotem: {config.IdTotem}, " +
                    $"Country: {config.Country}, " +
                    $"Business: {config.Business}, " +
                    $"Store: {config.Store}, " +
                    $"StoreId: {config.StoreId}"
                    );
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving configuration", ex);
                throw;
            }
        }

        public static ConfigurationData LoadConfiguration()
        {
            try
            {
                AppLogger.LogInfo("Loading configuration...");
                var config = new ConfigurationData
                {
                    IdTotem = System.Configuration.ConfigurationManager.AppSettings["IdTotem"] ?? "",
                    Country = System.Configuration.ConfigurationManager.AppSettings["Country"] ?? "",
                    Business = System.Configuration.ConfigurationManager.AppSettings["Business"] ?? "",
                    Store = System.Configuration.ConfigurationManager.AppSettings["Store"] ?? "",
                    StoreId = System.Configuration.ConfigurationManager.AppSettings["StoreId"] ?? ""
                };

                AppLogger.LogInfo($"Configuration loaded - IdTotem: {config.IdTotem}, Country: {config.Country}, Business: {config.Business}, Store: {config.Store}, StoreId: {config.StoreId}");
                return config;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading configuration. Returning default values", ex);
                return new ConfigurationData();
            }
        }

        public static bool IsConfigurationValid(ConfigurationData config)
        {
            bool isValid = !string.IsNullOrWhiteSpace(config.IdTotem) &&
                          !string.IsNullOrWhiteSpace(config.Country) &&
                          !string.IsNullOrWhiteSpace(config.Business) &&
                          !string.IsNullOrWhiteSpace(config.Store) &&
                          !string.IsNullOrWhiteSpace(config.StoreId);

            AppLogger.LogInfo($"Configuration validation: {(isValid ? "VALID" : "INVALID")}");
            return isValid;
        }
    }
}