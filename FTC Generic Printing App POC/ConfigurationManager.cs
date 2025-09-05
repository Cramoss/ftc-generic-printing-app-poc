using FTC_Generic_Printing_App_POC;
using System;
using System.Configuration;
using System.IO;

namespace FTC_Generic_Printing_App_POC
{
    public static class ConfigurationManager
    {
        #region Configuration Keys

        // Totem Configuration Keys
        private const string KEY_TOTEM_ID = "TotemId";
        private const string KEY_TOTEM_COUNTRY = "TotemCountry";
        private const string KEY_TOTEM_BUSINESS = "TotemBusiness";
        private const string KEY_TOTEM_STORE = "TotemStore";
        private const string KEY_TOTEM_STORE_ID = "TotemStoreId";

        // Store API Configuration Keys
        private const string KEY_STOREAPI_AUTH_URL = "StoreApi_AuthUrl";
        private const string KEY_STOREAPI_STORES_URL = "StoreApi_StoresUrl";
        private const string KEY_STOREAPI_CLIENT_ID = "StoreApi_ClientId";
        private const string KEY_STOREAPI_CLIENT_SECRET = "StoreApi_ClientSecret";

        // Firebase Configuration Keys
        private const string KEY_FIREBASE_DATABASE_URL = "Firebase_DatabaseUrl";
        private const string KEY_FIREBASE_PROJECT_ID = "Firebase_ProjectId";
        private const string KEY_FIREBASE_API_KEY = "Firebase_ApiKey";

        #endregion

        #region Load Configuration Methods

        public static ConfigurationData LoadTotemConfiguration()
        {
            try
            {
                AppLogger.LogInfo("Loading totem configuration...");
                var config = new ConfigurationData
                {
                    IdTotem = GetAppSetting(KEY_TOTEM_ID, ""),
                    Country = GetAppSetting(KEY_TOTEM_COUNTRY, ""),
                    Business = GetAppSetting(KEY_TOTEM_BUSINESS, ""),
                    Store = GetAppSetting(KEY_TOTEM_STORE, ""),
                    StoreId = GetAppSetting(KEY_TOTEM_STORE_ID, "")
                };

                AppLogger.LogInfo($"Totem configuration loaded. IdTotem: {config.IdTotem}, Country: {config.Country}, Business: {config.Business}, Store: {config.Store}, StoreId: {config.StoreId}");
                return config;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading totem configuration. Returning default values", ex);
                return new ConfigurationData();
            }
        }

        public static StoreApiConfig LoadStoreApiConfiguration()
        {
            try
            {
                AppLogger.LogInfo("Loading Store API configuration...");

                // Try to get settings from app.config
                var config = new StoreApiConfig
                {
                    AuthUrl = GetAppSetting(KEY_STOREAPI_AUTH_URL, ""),
                    StoresUrl = GetAppSetting(KEY_STOREAPI_STORES_URL, ""),
                    ClientId = GetAppSetting(KEY_STOREAPI_CLIENT_ID, ""),
                    ClientSecret = GetAppSetting(KEY_STOREAPI_CLIENT_SECRET, "")
                };

                // If any settings are empty, try to load from defaultConfig.xml
                if (string.IsNullOrEmpty(config.AuthUrl))
                    config.AuthUrl = GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_STORES_API_AUTH_URL);

                if (string.IsNullOrEmpty(config.StoresUrl))
                    config.StoresUrl = GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_STORES_API_URL);

                if (string.IsNullOrEmpty(config.ClientId))
                    config.ClientId = GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_STORES_API_CLIENT_ID);

                if (string.IsNullOrEmpty(config.ClientSecret))
                    config.ClientSecret = GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_STORES_API_CLIENT_SECRET);

                AppLogger.LogInfo("Store API configuration loaded successfully");
                return config;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading Store API configuration", ex);
                throw;
            }
        }

        public static FirebaseConfig LoadFirebaseConfiguration()
        {
            try
            {
                AppLogger.LogInfo("Loading Firebase configuration...");

                // Try to get settings from app.config
                var config = new FirebaseConfig
                {
                    DatabaseUrl = GetAppSetting(KEY_FIREBASE_DATABASE_URL, ""),
                    ProjectId = GetAppSetting(KEY_FIREBASE_PROJECT_ID, ""),
                    ApiKey = GetAppSetting(KEY_FIREBASE_API_KEY, ""),
                    DocumentPath = "tickets" // Hardcoded for now
                };

                // If any settings are empty, try to load from defaultConfig.xml
                if (string.IsNullOrEmpty(config.DatabaseUrl))
                    config.DatabaseUrl = GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_FIREBASE_DB_URL);

                if (string.IsNullOrEmpty(config.ProjectId))
                    config.ProjectId = GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_FIREBASE_PROJECT_ID);

                if (string.IsNullOrEmpty(config.ApiKey))
                    config.ApiKey = GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_FIREBASE_API_KEY);

                AppLogger.LogInfo("Firebase configuration loaded successfully");
                return config;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading Firebase configuration", ex);
                throw;
            }
        }

        public static ConfigurationData LoadConfiguration()
        {
            return LoadTotemConfiguration();
        }

        #endregion

        #region Save Configuration Methods

        public static void SaveTotemConfiguration(ConfigurationData config)
        {
            try
            {
                AppLogger.LogInfo("Saving Totem configuration...");
                var configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                UpdateOrAddSetting(configFile, KEY_TOTEM_ID, config.IdTotem);
                UpdateOrAddSetting(configFile, KEY_TOTEM_COUNTRY, config.Country);
                UpdateOrAddSetting(configFile, KEY_TOTEM_BUSINESS, config.Business);
                UpdateOrAddSetting(configFile, KEY_TOTEM_STORE, config.Store);
                UpdateOrAddSetting(configFile, KEY_TOTEM_STORE_ID, config.StoreId);

                configFile.Save(ConfigurationSaveMode.Modified);
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

                AppLogger.LogInfo($"Totem configuration saved successfully. " +
                    $"IdTotem: {config.IdTotem}, " +
                    $"Country: {config.Country}, " +
                    $"Business: {config.Business}, " +
                    $"Store: {config.Store}, " +
                    $"StoreId: {config.StoreId}"
                    );
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving Totem configuration", ex);
                throw;
            }
        }

        public static void SaveStoreApiConfiguration(string authUrl, string storesUrl, string clientId, string clientSecret)
        {
            try
            {
                AppLogger.LogInfo("Saving StoreApi configuration...");
                var configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                UpdateOrAddSetting(configFile, KEY_STOREAPI_AUTH_URL, authUrl);
                UpdateOrAddSetting(configFile, KEY_STOREAPI_STORES_URL, storesUrl);
                UpdateOrAddSetting(configFile, KEY_STOREAPI_CLIENT_ID, clientId);
                UpdateOrAddSetting(configFile, KEY_STOREAPI_CLIENT_SECRET, clientSecret);

                configFile.Save(ConfigurationSaveMode.Modified);
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

                // TODO: Log Store API settings
                AppLogger.LogInfo("StoreApi configuration saved successfully");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving StoreApi configuration", ex);
                throw;
            }
        }

        public static void SaveFirebaseConfiguration(string databaseUrl, string projectId, string apiKey)
        {
            try
            {
                AppLogger.LogInfo("Saving Firebase configuration...");
                var configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                UpdateOrAddSetting(configFile, KEY_FIREBASE_DATABASE_URL, databaseUrl);
                UpdateOrAddSetting(configFile, KEY_FIREBASE_PROJECT_ID, projectId);
                UpdateOrAddSetting(configFile, KEY_FIREBASE_API_KEY, apiKey);

                configFile.Save(ConfigurationSaveMode.Modified);
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

                AppLogger.LogInfo("Firebase configuration saved successfully");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving Firebase configuration", ex);
                throw;
            }
        }

        public static void SaveConfiguration(ConfigurationData config)
        {
            SaveTotemConfiguration(config);
        }

        #endregion

        #region Helper Methods

        private static string GetAppSetting(string key, string defaultValue)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key] ?? defaultValue;
        }

        private static void UpdateOrAddSetting(System.Configuration.Configuration configFile, string key, string value)
        {
            if (configFile.AppSettings.Settings[key] == null)
            {
                configFile.AppSettings.Settings.Add(key, value);
            }
            else
            {
                configFile.AppSettings.Settings[key].Value = value;
            }
        }

        private static string GetValueFromDefaultConfig(string key)
        {
            try
            {
                string defaultConfigPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "defaultConfig.xml");

                if (File.Exists(defaultConfigPath))
                {
                    var configXml = new System.Xml.XmlDocument();
                    configXml.Load(defaultConfigPath);

                    var node = configXml.SelectSingleNode($"//appSettings/add[@key='{key}']");
                    if (node != null)
                    {
                        string value = node.Attributes["value"]?.Value;
                        if (!string.IsNullOrEmpty(value))
                        {
                            AppLogger.LogInfo($"Loaded default value for {key} from defaultConfig.xml");
                            return value;
                        }
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                AppLogger.LogWarning($"Could not load default value for {key}: {ex.Message}");
                return string.Empty;
            }
        }

        #endregion

        #region Validation Methods

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

        #endregion
    }

    public class StoreApiConfig
    {
        public string AuthUrl { get; set; }
        public string StoresUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class FirebaseConfig
    {
        public string DatabaseUrl { get; set; }
        public string ProjectId { get; set; }
        public string ApiKey { get; set; }
        public string DocumentPath { get; set; }
    }
}