using FTC_Generic_Printing_App_POC.Models;
using FTC_Generic_Printing_App_POC.Models.Settings;
using FTC_Generic_Printing_App_POC.Utils;
using System;
using System.Configuration;
using System.IO;

namespace FTC_Generic_Printing_App_POC.Manager
{
    public static class SettingsManager
    {
        #region Fields
        // Totem settings Keys
        private const string KEY_TOTEM_ID = "TotemId";
        private const string KEY_TOTEM_COUNTRY = "TotemCountry";
        private const string KEY_TOTEM_BUSINESS = "TotemBusiness";
        private const string KEY_TOTEM_STORE = "TotemStore";
        private const string KEY_TOTEM_STORE_ID = "TotemStoreId";

        // Stores API settings Keys
        private const string KEY_STORESAPI_AUTH_URL = "StoresApi_AuthUrl";
        private const string KEY_STORESAPI_STORES_URL = "StoresApi_StoresUrl";
        private const string KEY_STORESAPI_CLIENT_ID = "StoresApi_ClientId";
        private const string KEY_STORESAPI_CLIENT_SECRET = "StoresApi_ClientSecret  ";

        // Firebase settings Keys
        private const string KEY_FIREBASE_DATABASE_URL = "Firebase_DatabaseUrl";
        private const string KEY_FIREBASE_PROJECT_ID = "Firebase_ProjectId";
        private const string KEY_FIREBASE_API_KEY = "Firebase_ApiKey";

        // Admin Password Key
        private const string KEY_ADMIN_PASSWORD = "AdminPassword";
        #endregion

        #region Properties

        private static string UserConfigPath
        {
            get
            {
                string appDataPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "FTC Generic Printing App POC"); // TODO: Remove POC on final version

                if (!Directory.Exists(appDataPath))
                {
                    try
                    {
                        Directory.CreateDirectory(appDataPath);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.LogError($"Failed to create application data folder: {appDataPath}", ex);
                    }
                }

                return Path.Combine(appDataPath, "user.config");
            }
        }

        private static string DefaultSettingsPath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "defaultSettings.xml");
            }
        }
        #endregion

        #region Initialization
        // Initializes the app.config file with default values from defaultSettings.xml
        public static void InitializeSettings()
        {
            try
            {
                AppLogger.LogInfo("Initializing application settings");

                InitializeStoresApiSettings();
                InitializeFirebaseSettings();
                InitializeAdminPassword();

                AppLogger.LogInfo("Application settings initialization completed");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error initializing application settings", ex);
            }
        }

        private static void InitializeStoresApiSettings()
        {
            var storesApiSettings = LoadStoresApiSettings();

            bool needsUpdate = string.IsNullOrEmpty(storesApiSettings.AuthUrl) ||
                               string.IsNullOrEmpty(storesApiSettings.StoresUrl) ||
                               string.IsNullOrEmpty(storesApiSettings.ClientId) ||
                               string.IsNullOrEmpty(storesApiSettings.ClientSecret);

            if (needsUpdate)
            {
                AppLogger.LogInfo("Stores API settings not found in app.config. Loading from default config file");

                string authUrl = GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_STORES_API_AUTH_URL);
                string storesUrl = GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_STORES_API_URL);
                string clientId = GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_STORES_API_CLIENT_ID);
                string clientSecret = GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_STORES_API_CLIENT_SECRET);

                SaveStoresApiSettings(
                    string.IsNullOrEmpty(storesApiSettings.AuthUrl) ? authUrl : storesApiSettings.AuthUrl,
                    string.IsNullOrEmpty(storesApiSettings.StoresUrl) ? storesUrl : storesApiSettings.StoresUrl,
                    string.IsNullOrEmpty(storesApiSettings.ClientId) ? clientId : storesApiSettings.ClientId,
                    string.IsNullOrEmpty(storesApiSettings.ClientSecret) ? clientSecret : storesApiSettings.ClientSecret
                );

                AppLogger.LogInfo("Stores API default settings saved");
            }
        }

        private static void InitializeFirebaseSettings()
        {
            var config = LoadFirebaseSettings();

            bool needsUpdate = string.IsNullOrEmpty(config.DatabaseUrl) ||
                               string.IsNullOrEmpty(config.ProjectId) ||
                               string.IsNullOrEmpty(config.ApiKey);

            if (needsUpdate)
            {
                AppLogger.LogInfo("Firebase settings not found. Loading from default config file");

                string databaseUrl = GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_FIREBASE_DB_URL);
                string projectId = GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_FIREBASE_PROJECT_ID);
                string apiKey = GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_FIREBASE_API_KEY);

                SaveFirebaseSettings(
                    string.IsNullOrEmpty(config.DatabaseUrl) ? databaseUrl : config.DatabaseUrl,
                    string.IsNullOrEmpty(config.ProjectId) ? projectId : config.ProjectId,
                    string.IsNullOrEmpty(config.ApiKey) ? apiKey : config.ApiKey
                );

                AppLogger.LogInfo("Firebase default settings saved");
            }
        }

        private static void InitializeAdminPassword()
        {
            string currentPassword = GetAppSetting(KEY_ADMIN_PASSWORD, null);

            if (string.IsNullOrEmpty(currentPassword))
            {
                AppLogger.LogInfo("Admin password not found, loading from default config");

                string defaultPassword = GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_ADMIN_PASSWORD);

                try
                {
                    var settingsFile = GetSettingsFile();
                    UpdateOrAddSetting(settingsFile, KEY_ADMIN_PASSWORD, defaultPassword);
                    settingsFile.Save(ConfigurationSaveMode.Modified);

                    AppLogger.LogInfo("Admin password default settings saved");
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("Error saving admin password default settings", ex);
                }
            }
        }
        #endregion

        #region Helper Methods
        private static Configuration GetSettingsFile()
        {
            try
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = UserConfigPath
                };

                return ConfigurationManager.OpenMappedExeConfiguration(
                    fileMap, ConfigurationUserLevel.None);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error opening settings file", ex);
                throw;
            }
        }

        private static string GetAppSetting(string key, string defaultValue)
        {
            try
            {
                var configFile = GetSettingsFile();
                var settings = configFile.AppSettings.Settings;
                return settings[key]?.Value ?? defaultValue;
            }
            catch (Exception ex)
            {
                AppLogger.LogWarning($"Error reading setting {key} from user config, falling back to app.config: {ex.Message}");
                return ConfigurationManager.AppSettings[key] ?? defaultValue;
            }
        }

        private static void UpdateOrAddSetting(Configuration settingsFile, string key, string value)
        {
            if (settingsFile.AppSettings.Settings[key] == null)
            {
                settingsFile.AppSettings.Settings.Add(key, value);
            }
            else
            {
                settingsFile.AppSettings.Settings[key].Value = value;
            }
        }

        public static bool AreSettingsValid(Totem totem)
        {
            bool isValid = !string.IsNullOrWhiteSpace(totem.IdTotem) &&
                          !string.IsNullOrWhiteSpace(totem.Country) &&
                          !string.IsNullOrWhiteSpace(totem.Business) &&
                          !string.IsNullOrWhiteSpace(totem.Store) &&
                          !string.IsNullOrWhiteSpace(totem.StoreId);

            AppLogger.LogInfo($"Totem settings: {(isValid ? "VALID" : "INVALID")}");
            return isValid;
        }

        public static string GetValueFromDefaultSettings(string key)
        {
            try
            {
                if (File.Exists(DefaultSettingsPath))
                {
                    AppLogger.LogInfo($"defaultSettings.xml found at: {DefaultSettingsPath}");

                    var configXml = new System.Xml.XmlDocument();
                    configXml.Load(DefaultSettingsPath);

                    var node = configXml.SelectSingleNode($"//appSettings/add[@key='{key}']");
                    if (node != null)
                    {
                        string value = node.Attributes["value"]?.Value;
                        if (!string.IsNullOrEmpty(value))
                        {
                            AppLogger.LogInfo($"Loaded default value for {key} from defaultSettings.xml");
                            return value;
                        }
                    }
                    else
                    {
                        AppLogger.LogWarning($"Key '{key}' not found in defaultSettings.xml");
                    }
                }
                else
                {
                    AppLogger.LogWarning($"defaultSettings.xml not found at path: {DefaultSettingsPath}");
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

        #region Load Methods
        public static Totem LoadTotemSettings()
        {
            try
            {
                AppLogger.LogInfo("Loading Totem settings...");
                var totem = new Totem()
                {
                    IdTotem = GetAppSetting(KEY_TOTEM_ID, ""),
                    Country = GetAppSetting(KEY_TOTEM_COUNTRY, ""),
                    Business = GetAppSetting(KEY_TOTEM_BUSINESS, ""),
                    Store = GetAppSetting(KEY_TOTEM_STORE, ""),
                    StoreId = GetAppSetting(KEY_TOTEM_STORE_ID, "")
                };

                if (!AreSettingsValid(totem))
                {
                    AppLogger.LogWarning("Totem settings are incomplete or invalid. Please check the settings or configure a new Totem");
                }
                else
                {
                    AppLogger.LogInfo($"Totem settings loaded. " +
                        $"IdTotem: {totem.IdTotem}, " +
                        $"Country: {totem.Country}, " +
                        $"Business: {totem.Business}, " +
                        $"Store: {totem.Store}, " +
                        $"StoreId: {totem.StoreId}");
                }

                return totem;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading Totem settings. Returning default values", ex);
                return new Totem();
            }
        }

        public static StoresApiSettings LoadStoresApiSettings()
        {
            try
            {
                AppLogger.LogInfo("Loading Stores API settings...");

                var config = new StoresApiSettings
                {
                    AuthUrl = GetAppSetting(KEY_STORESAPI_AUTH_URL, ""),
                    StoresUrl = GetAppSetting(KEY_STORESAPI_STORES_URL, ""),
                    ClientId = GetAppSetting(KEY_STORESAPI_CLIENT_ID, ""),
                    ClientSecret = GetAppSetting(KEY_STORESAPI_CLIENT_SECRET, "")
                };

                AppLogger.LogInfo("Stores API settings loaded successfully");
                return config;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading Stores API settings", ex);
                throw;
            }
        }

        public static FirebaseSettings LoadFirebaseSettings()
        {
            try
            {
                AppLogger.LogInfo("Loading Firebase settings...");

                var config = new FirebaseSettings
                {
                    DatabaseUrl = GetAppSetting(KEY_FIREBASE_DATABASE_URL, ""),
                    ProjectId = GetAppSetting(KEY_FIREBASE_PROJECT_ID, ""),
                    ApiKey = GetAppSetting(KEY_FIREBASE_API_KEY, ""),
                    DocumentPath = "tickets" // Hardcoded for now, might change in the future
                };

                AppLogger.LogInfo("Firebase settings loaded successfully");
                return config;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading Firebase settings", ex);
                throw;
            }
        }

        public static string GetAdminPassword()
        {
            return GetAppSetting(KEY_ADMIN_PASSWORD, "");
        }
        #endregion

        #region Save Methods
        public static void SaveTotemSettings(Totem totem)
        {
            try
            {
                AppLogger.LogInfo("Saving Totem settings...");
                var configFile = GetSettingsFile();

                UpdateOrAddSetting(configFile, KEY_TOTEM_ID, totem.IdTotem);
                UpdateOrAddSetting(configFile, KEY_TOTEM_COUNTRY, totem.Country);
                UpdateOrAddSetting(configFile, KEY_TOTEM_BUSINESS, totem.Business);
                UpdateOrAddSetting(configFile, KEY_TOTEM_STORE, totem.Store);
                UpdateOrAddSetting(configFile, KEY_TOTEM_STORE_ID, totem.StoreId);

                configFile.Save(ConfigurationSaveMode.Modified);

                AppLogger.LogInfo($"Totem settings saved successfully to {UserConfigPath}. " +
                    $"IdTotem: {totem.IdTotem}, " +
                    $"Country: {totem.Country}, " +
                    $"Business: {totem.Business}, " +
                    $"Store: {totem.Store}, " +
                    $"StoreId: {totem.StoreId}"
                    );
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving Totem settings", ex);
                throw;
            }
        }

        public static void SaveStoresApiSettings(string authUrl, string storesUrl, string clientId, string clientSecret)
        {
            try
            {
                AppLogger.LogInfo("Saving Stores API settings...");
                var settingsFile = GetSettingsFile();

                UpdateOrAddSetting(settingsFile, KEY_STORESAPI_AUTH_URL, authUrl);
                UpdateOrAddSetting(settingsFile, KEY_STORESAPI_STORES_URL, storesUrl);
                UpdateOrAddSetting(settingsFile, KEY_STORESAPI_CLIENT_ID, clientId);
                UpdateOrAddSetting(settingsFile, KEY_STORESAPI_CLIENT_SECRET, clientSecret);

                settingsFile.Save(ConfigurationSaveMode.Modified);

                AppLogger.LogInfo($"Stores API settings saved successfully to {UserConfigPath}. URL: {storesUrl}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving Stores API settings", ex);
                throw;
            }
        }

        public static void SaveFirebaseSettings(string databaseUrl, string projectId, string apiKey)
        {
            try
            {
                AppLogger.LogInfo("Saving Firebase settings...");
                var settingsFile = GetSettingsFile();

                UpdateOrAddSetting(settingsFile, KEY_FIREBASE_DATABASE_URL, databaseUrl);
                UpdateOrAddSetting(settingsFile, KEY_FIREBASE_PROJECT_ID, projectId);
                UpdateOrAddSetting(settingsFile, KEY_FIREBASE_API_KEY, apiKey);

                settingsFile.Save(ConfigurationSaveMode.Modified);

                AppLogger.LogInfo($"Firebase settings saved successfully to {UserConfigPath}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving Firebase settings", ex);
                throw;
            }
        }

        public static void SaveAdminPassword(string password)
        {
            try
            {
                AppLogger.LogInfo("Saving admin password...");
                var configFile = GetSettingsFile();

                UpdateOrAddSetting(configFile, KEY_ADMIN_PASSWORD, password);

                configFile.Save(ConfigurationSaveMode.Modified);

                AppLogger.LogInfo($"Admin password saved successfully to {UserConfigPath}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving admin password", ex);
                throw;
            }
        }

        public static void SaveSettings(Totem totem)
        {
            SaveTotemSettings(totem);
        }
        #endregion
    }
}