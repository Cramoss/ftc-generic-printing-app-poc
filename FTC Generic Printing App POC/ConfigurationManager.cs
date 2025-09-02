using NLog;
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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void SaveConfiguration(ConfigurationData config)
        {
            try
            {
                logger.Info("Saving configuration...");
                var configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Remove existing keys if they exist
                configFile.AppSettings.Settings.Remove("IdTotem");
                configFile.AppSettings.Settings.Remove("Country");
                configFile.AppSettings.Settings.Remove("Business");
                configFile.AppSettings.Settings.Remove("Store");
                configFile.AppSettings.Settings.Remove("StoreId");

                // Add new values
                configFile.AppSettings.Settings.Add("IdTotem", config.IdTotem);
                configFile.AppSettings.Settings.Add("Country", config.Country);
                configFile.AppSettings.Settings.Add("Business", config.Business);
                configFile.AppSettings.Settings.Add("Store", config.Store);
                configFile.AppSettings.Settings.Add("StoreId", config.StoreId);

                configFile.Save(ConfigurationSaveMode.Modified);
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

                logger.Info($"Configuration saved successfully - IdTotem: {config.IdTotem}, Country: {config.Country}, Business: {config.Business}, Store: {config.Store}, StoreId: {config.StoreId}");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error saving configuration");
                throw;
            }
        }

        public static ConfigurationData LoadConfiguration()
        {
            try
            {
                logger.Info("Loading configuration...");
                var config = new ConfigurationData
                {
                    IdTotem = System.Configuration.ConfigurationManager.AppSettings["IdTotem"] ?? "",
                    Country = System.Configuration.ConfigurationManager.AppSettings["Country"] ?? "",
                    Business = System.Configuration.ConfigurationManager.AppSettings["Business"] ?? "",
                    Store = System.Configuration.ConfigurationManager.AppSettings["Store"] ?? "",
                    StoreId = System.Configuration.ConfigurationManager.AppSettings["StoreId"] ?? ""
                };

                logger.Info($"Configuration loaded - IdTotem: {config.IdTotem}, Country: {config.Country}, Business: {config.Business}, Store: {config.Store}, StoreId: {config.StoreId}");
                return config;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error loading configuration, returning defaults");
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

            logger.Info($"Configuration validation: {(isValid ? "VALID" : "INVALID")}");
            return isValid;
        }
    }
}