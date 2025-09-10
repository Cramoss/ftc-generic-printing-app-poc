using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System.Configuration;

namespace FTC_Generic_Printing_App_POC
{
    public class FirebaseService
    {
        #region Fields
        private FirebaseClient firebaseClient;
        private string firebaseUrl;
        private string projectId;
        private string apiKey;
        private readonly String databaseDocumentParentPath = "tickets";
        private readonly String databaseDocumentTestingPath = "connection-test";
        private ConfigurationData currentTotemConfig;
        private bool isListening = false;
        private IDisposable currentSubscription;
        #endregion

        #region Initialization
        public FirebaseService()
        {
            var config = ConfigurationManager.LoadFirebaseConfiguration();
            firebaseUrl = config.DatabaseUrl;
            projectId = config.ProjectId;
            apiKey = config.ApiKey;
            databaseDocumentParentPath = config.DocumentPath;
            LoadCurrentConfiguration();
            InitializeFirebase();
            AppLogger.LogInfo($"FirebaseManager initialized for project: {projectId}");
        }

        private void LoadCurrentConfiguration()
        {
            try
            {
                currentTotemConfig = FTC_Generic_Printing_App_POC.ConfigurationManager.LoadTotemConfiguration();
                AppLogger.LogInfo($"Loaded current Totem configuration. Totem: {currentTotemConfig.IdTotem}, Store: {currentTotemConfig.StoreId}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading current Totem configuration", ex);
                currentTotemConfig = new ConfigurationData();
            }
        }

        public void RefreshConfiguration()
        {
            try
            {
                var previousConfig = currentTotemConfig;
                LoadCurrentConfiguration();

                if (isListening && !ConfigurationEquals(previousConfig, currentTotemConfig))
                {
                    AppLogger.LogInfo("Configuration changed. Restarting Firebase listener");
                    StopListening();
                    StartListeningAsync();
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error refreshing configuration", ex);
            }
        }
        #endregion

        #region Core Methods
        private string LoadConfigurationWithFallback(string key, string defaultKeyName)
        {
            string value = System.Configuration.ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
            {
                try
                {
                    string defaultConfigPath = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "defaultConfig.xml");

                    if (System.IO.File.Exists(defaultConfigPath))
                    {
                        var configXml = new System.Xml.XmlDocument();
                        configXml.Load(defaultConfigPath);

                        var node = configXml.SelectSingleNode($"//appSettings/add[@key='{defaultKeyName}']");
                        if (node != null)
                        {
                            value = node.Attributes["value"]?.Value;
                            AppLogger.LogInfo($"Loaded default value for {key} from defaultConfig.xml");
                        }
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.LogWarning($"Could not load default value for {key} from defaultConfig.xml: {ex.Message}");
                }
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"{key} not configured");
            }

            return value;
        }

        public void ReloadConfiguration()
        {
            try
            {
                AppLogger.LogInfo("Reloading Firebase configuration");
                var config = ConfigurationManager.LoadFirebaseConfiguration();

                firebaseUrl = config.DatabaseUrl;
                projectId = config.ProjectId;
                apiKey = config.ApiKey;

                InitializeFirebase();

                AppLogger.LogInfo("Firebase configuration reloaded successfully");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Failed to reload Firebase configuration", ex);
            }
        }

        // Listener
        private void InitializeFirebase()
        {
            try
            {
                var options = new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(apiKey)
                };

                firebaseClient = new FirebaseClient(firebaseUrl, options);
                AppLogger.LogInfo($"Firebase client initialized with API key authentication");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error initializing Firebase client", ex);
                throw;
            }
        }

        public async Task StartListeningAsync()
        {
            try
            {
                if (isListening)
                {
                    AppLogger.LogWarning("Firebase listener already active");
                    return;
                }

                if (!IsConfigurationValid())
                {
                    AppLogger.LogError("Cannot start Firebase listener. Configuration is invalid");
                    return;
                }

                string fullPath = BuildDocumentPath();
                AppLogger.LogInfo($"Starting Firebase listener on path: {fullPath}");

                var observable = firebaseClient
                    .Child(fullPath)
                    .AsObservable<Dictionary<string, object>>();

                currentSubscription = observable.Subscribe(
                    onNext: (firebaseEvent) =>
                    {
                        if (firebaseEvent != null && firebaseEvent.Object != null)
                        {
                            // Only process if this is a new ticket that was added
                            if (firebaseEvent.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                            {
                                string key = firebaseEvent.Key;
                                var data = firebaseEvent.Object;

                                AppLogger.LogFirebaseEvent("NEW_TICKET",
                                    $"New ticket detected: Key={key}, Data={JsonConvert.SerializeObject(data)}");

                                // TODO: Process, map and print ticket
                            }
                        }
                    },
                    onError: (error) =>
                    {
                        AppLogger.LogError("Firebase listener error", error);
                        isListening = false;
                        currentSubscription?.Dispose();

                        // Try to reconnect after delay
                        Task.Delay(5000).ContinueWith(_ => StartListeningAsync());
                    }
                );

                isListening = true;
                AppLogger.LogFirebaseEvent("LISTENER_STARTED", $"Listening on path: {fullPath}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error starting Firebase listener", ex);
                isListening = false;
                throw;
            }
        }

        public void StopListening()
        {
            try
            {
                if (!isListening)
                {
                    AppLogger.LogInfo("Firebase listener not active");
                    return;
                }

                currentSubscription?.Dispose();
                currentSubscription = null;

                isListening = false;
                AppLogger.LogFirebaseEvent("LISTENER_STOPPED", "Firebase listener stopped");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error stopping Firebase listener", ex);
            }
        }

        public void Dispose()
        {
            try
            {
                StopListening();
                firebaseClient?.Dispose();
                AppLogger.LogInfo("FirebaseManager disposed");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error disposing FirebaseManager", ex);
            }
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                AppLogger.LogInfo("Testing Firebase connection...");

                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var testUrl = $"{firebaseUrl}/{databaseDocumentTestingPath}.json?auth={apiKey}";
                    AppLogger.LogInfo($"Testing Firebase REST API: {firebaseUrl}");

                    var response = await httpClient.GetAsync(testUrl);
                    if (!response.IsSuccessStatusCode)
                    {
                        AppLogger.LogError($"Firebase REST API test failed: {response.StatusCode}");
                        return false;
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    AppLogger.LogInfo($"Firebase REST API response: {content.Substring(0, Math.Min(200, content.Length))}...");
                }

                var testPath = $"{databaseDocumentTestingPath}-{DateTime.Now.Ticks}";
                var testData = new
                {
                    timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    test = true,
                    apiKey = apiKey.Substring(0, 8) + "..."
                };

                AppLogger.LogInfo($"Testing Firebase CRUD with test path: {testPath}");

                await firebaseClient
                    .Child(testPath)
                    .PutAsync(testData);

                AppLogger.LogInfo("Firebase write test successful");

                var result = await firebaseClient
                    .Child(testPath)
                    .OnceSingleAsync<object>();

                if (result == null)
                {
                    AppLogger.LogError("Firebase read test failed. Null response");
                    return false;
                }

                AppLogger.LogInfo("Firebase read test successful");

                await firebaseClient
                    .Child(testPath)
                    .DeleteAsync();

                AppLogger.LogInfo("Firebase delete test successful");
                AppLogger.LogInfo("Firebase connection test successful");
                return true;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Firebase connection test failed", ex);
                return false;
            }
        }

        public async Task<bool> TestAuthenticationAsync()
        {
            try
            {
                AppLogger.LogInfo("Testing Firebase API key authentication...");

                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var authTestUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}";

                    var requestBody = new
                    {
                        returnSecureToken = false
                    };

                    var jsonContent = new System.Net.Http.StringContent(
                        Newtonsoft.Json.JsonConvert.SerializeObject(requestBody),
                        System.Text.Encoding.UTF8,
                        "application/json"
                    );

                    var response = await httpClient.PostAsync(authTestUrl, jsonContent);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        if (responseContent.Contains("API_KEY_INVALID") || responseContent.Contains("INVALID_API_KEY"))
                        {
                            AppLogger.LogError($"Configured Firebase API key is invalid: {responseContent}");
                            return false;
                        }
                        else
                        {
                            AppLogger.LogInfo($"Configured Firebase API key appears valid");
                            return true;
                        }
                    }
                    else if (response.IsSuccessStatusCode)
                    {
                        AppLogger.LogInfo("Firebase API key authentication test successful");
                        return true;
                    }
                    else
                    {
                        AppLogger.LogError($"Firebase API key test failed: {response.StatusCode} - {responseContent}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Firebase authentication test failed", ex);
                return false;
            }
        }

        public async Task<bool> TestDocumentPathAsync()
        {
            try
            {
                if (!IsConfigurationValid())
                {
                    AppLogger.LogError("Cannot test document path. Configuration is invalid");
                    return false;
                }

                string fullPath = BuildDocumentPath();
                AppLogger.LogInfo($"Testing Firebase document path: {fullPath}");

                var result = await firebaseClient
                    .Child(fullPath)
                    .OnceSingleAsync<object>();

                AppLogger.LogInfo("Firebase document path test successful");
                return true;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Firebase document path test failed. Path may not exist yet", ex);
                return false;
            }
        }
        #endregion

        #region Helper methods
        private bool ConfigurationEquals(ConfigurationData config1, ConfigurationData config2)
        {
            if (config1 == null || config2 == null) return false;
            return config1.IdTotem == config2.IdTotem &&
                   config1.Country == config2.Country &&
                   config1.Business == config2.Business &&
                   config1.StoreId == config2.StoreId;
        }

        public string BuildDocumentPath()
        {
            try
            {
                if (!IsConfigurationValid())
                {
                    throw new InvalidOperationException("Configuration is not valid for building Firebase path");
                }

                string countryCode = MapCountryToCode(currentTotemConfig.Country);
                string path = $"{databaseDocumentParentPath}/{countryCode}/{currentTotemConfig.Business.ToLower()}/{currentTotemConfig.StoreId}/{currentTotemConfig.IdTotem}";
                AppLogger.LogInfo($"Built Firebase document path: {path}");
                return path;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error building Firebase document path", ex);
                throw;
            }
        }

        private string MapCountryToCode(string country)
        {
            if (string.IsNullOrEmpty(country))
                return "unknown";

            string normalizedCountry = country.Trim().ToLower();

            switch (normalizedCountry)
            {
                case "chile":
                    return "cl";
                case "peru":
                case "perú":
                    return "pe";
                case "colombia":
                    return "co";
                default:
                    if (normalizedCountry == "cl" || normalizedCountry == "pe" || normalizedCountry == "co")
                        return normalizedCountry;

                    AppLogger.LogWarning($"Unknown country format '{country}', using normalized value: {normalizedCountry}");
                    return normalizedCountry;
            }
        }
        private bool IsConfigurationValid()
        {
            if (currentTotemConfig == null)
            {
                AppLogger.LogError("Configuration validation failed: currentConfig is null");
                return false;
            }

            bool isValid = !string.IsNullOrWhiteSpace(currentTotemConfig.IdTotem) &&
                          !string.IsNullOrWhiteSpace(currentTotemConfig.Country) &&
                          !string.IsNullOrWhiteSpace(currentTotemConfig.Business) &&
                          !string.IsNullOrWhiteSpace(currentTotemConfig.StoreId);

            if (!isValid)
            {
                AppLogger.LogError($"Configuration validation failed: IdTotem={currentTotemConfig.IdTotem ?? "null"}, " +
                                  $"Country={currentTotemConfig.Country ?? "null"}, " +
                                  $"Business={currentTotemConfig.Business ?? "null"}, " +
                                  $"StoreId={currentTotemConfig.StoreId ?? "null"}");
            }

            return isValid;
        }

        public ConfigurationData GetCurrentConfiguration()
        {
            return currentTotemConfig;
        }
        #endregion

        public bool IsListening => isListening;

        /// Reloads the totem configuration and restarts the Firebase listener if needed
        public void ReloadTotemConfiguration()
        {
            try
            {
                AppLogger.LogInfo("Reloading totem configuration...");
                var previousConfig = currentTotemConfig;
                LoadCurrentConfiguration();

                AppLogger.LogInfo($"Reloaded totem configuration: " +
                    $"IdTotem={currentTotemConfig?.IdTotem ?? "null"}, " +
                    $"Country={currentTotemConfig?.Country ?? "null"}, " +
                    $"Business={currentTotemConfig?.Business ?? "null"}, " +
                    $"StoreId={currentTotemConfig?.StoreId ?? "null"}");

                if (!ConfigurationEquals(previousConfig, currentTotemConfig))
                {
                    if (isListening)
                    {
                        AppLogger.LogInfo("Configuration changed. Restarting Firebase listener");
                        StopListening();
                    }

                    if (IsConfigurationValid())
                    {
                        AppLogger.LogInfo("Starting Firebase listener with new configuration");
                        Task.Run(() => StartListeningAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error reloading totem configuration", ex);
            }
        }
    }
}