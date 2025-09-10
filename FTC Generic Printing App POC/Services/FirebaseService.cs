using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;

namespace FTC_Generic_Printing_App_POC
{
    public class FirebaseService : IDisposable
    {
        #region Fields
        private FirebaseClient firebaseClient;
        private string firebaseUrl;
        private string projectId;
        private string apiKey;
        private string databaseDocumentParentPath = "tickets";
        private readonly string databaseDocumentTestingPath = "connection-test";
        private ConfigurationData currentTotemConfig;
        private bool isListening = false;
        private IDisposable currentSubscription;
        #endregion

        #region Initialization
        public FirebaseService()
        {
            LoadFirebaseSettings();
            LoadCurrentConfiguration();
            InitializeFirebase();
            AppLogger.LogInfo($"FirebaseManager initialized for project: {projectId}");
        }

        private void LoadFirebaseSettings()
        {
            var config = ConfigurationManager.LoadFirebaseConfiguration();
            firebaseUrl = config.DatabaseUrl;
            projectId = config.ProjectId;
            apiKey = config.ApiKey;
            databaseDocumentParentPath = config.DocumentPath;
        }

        private void LoadCurrentConfiguration()
        {
            try
            {
                currentTotemConfig = ConfigurationManager.LoadTotemConfiguration();
                AppLogger.LogInfo($"Loaded current Totem configuration. Totem: {currentTotemConfig.IdTotem}, Store: {currentTotemConfig.StoreId}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading current Totem configuration", ex);
                currentTotemConfig = new ConfigurationData();
            }
        }

        private void InitializeFirebase()
        {
            try
            {
                firebaseClient = new FirebaseClient($"{firebaseUrl}?auth={apiKey}");
                AppLogger.LogInfo($"Firebase client initialized with API key authentication");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error initializing Firebase client", ex);
                throw;
            }
        }
        #endregion

        #region Public Methods
        public void ReloadConfiguration()
        {
            try
            {
                AppLogger.LogInfo("Reloading Firebase configuration");
                LoadFirebaseSettings();
                InitializeFirebase();
                AppLogger.LogInfo("Firebase configuration reloaded successfully");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Failed to reload Firebase configuration", ex);
            }
        }

        // Reloads the totem configuration and restarts the Firebase listener using the new Totem if needed
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

                    if (IsTotemConfigurationValid())
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

        public async Task StartListeningAsync()
        {
            try
            {
                if (isListening)
                {
                    AppLogger.LogWarning("Firebase listener already active");
                    return;
                }

                if (!IsTotemConfigurationValid())
                {
                    AppLogger.LogError("Cannot start Firebase listener. Totem configuration is invalid");
                    return;
                }

                string fullPath = BuildDocumentPath();
                AppLogger.LogInfo($"Starting Firebase listener on path: {fullPath}");

                // Pre-check path and create placeholder if needed
                try
                {
                    if (!await CheckPathExistsAsync(fullPath))
                    {
                        await CreatePathPlaceholderAsync(fullPath);
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.LogWarning($"Path preparation error: {ex.Message}");
                }

                var observable = firebaseClient
                    .Child(fullPath)
                    .AsObservable<Dictionary<string, object>>();

                currentSubscription = observable.Subscribe(
                    firebaseEvent => HandleFirebaseEvent(firebaseEvent),
                    error => HandleFirebaseError(error)
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

        // Tests Firebase connection and its API key
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                AppLogger.LogInfo("Testing Firebase connection...");

                // Test REST API access
                using (var httpClient = new HttpClient())
                {
                    var testUrl = $"{firebaseUrl}/{databaseDocumentTestingPath}.json?auth={apiKey}";
                    var response = await httpClient.GetAsync(testUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        AppLogger.LogError($"Firebase REST API test failed: {response.StatusCode}");
                        return false;
                    }
                }

                // Test CRUD operations
                var testPath = $"{databaseDocumentTestingPath}-{DateTime.Now.Ticks}";
                var testData = new { timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(), test = true };

                await firebaseClient.Child(testPath).PutAsync(testData);
                var result = await firebaseClient.Child(testPath).OnceSingleAsync<object>();

                if (result == null)
                {
                    AppLogger.LogError("Firebase read test failed");
                    return false;
                }

                await firebaseClient.Child(testPath).DeleteAsync();

                AppLogger.LogInfo("Firebase connection test successful");
                return true;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Firebase connection test failed", ex);
                return false;
            }
        }

        // Tests Firebase API key authentication
        public async Task<bool> TestAuthenticationAsync()
        {
            try
            {
                AppLogger.LogInfo("Testing Firebase API key authentication...");

                using (var httpClient = new HttpClient())
                {
                    var authTestUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}";
                    var requestBody = new { returnSecureToken = false };
                    var jsonContent = new StringContent(
                        JsonConvert.SerializeObject(requestBody),
                        System.Text.Encoding.UTF8,
                        "application/json"
                    );

                    var response = await httpClient.PostAsync(authTestUrl, jsonContent);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // API key validation returns BadRequest with specific message when key is valid
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest &&
                        !responseContent.Contains("API_KEY_INVALID") &&
                        !responseContent.Contains("INVALID_API_KEY"))
                    {
                        AppLogger.LogInfo("Firebase API key appears valid");
                        return true;
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        AppLogger.LogInfo("Firebase API key authentication successful");
                        return true;
                    }

                    AppLogger.LogError($"Firebase API key test failed: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Firebase authentication test failed", ex);
                return false;
            }
        }

        // Tests if the configured document path exists and is accessible
        public async Task<bool> TestDocumentPathAsync()
        {
            try
            {
                if (!IsTotemConfigurationValid())
                {
                    AppLogger.LogError("Cannot test document path. Configuration is invalid");
                    return false;
                }

                string fullPath = BuildDocumentPath();
                AppLogger.LogInfo($"Testing Firebase document path: {fullPath}");

                using (var httpClient = new HttpClient())
                {
                    var testUrl = $"{firebaseUrl}/{fullPath}.json?auth={apiKey}";
                    var response = await httpClient.GetAsync(testUrl);

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        AppLogger.LogError("Firebase path access error: Authentication failed");
                        return false;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                            (response.IsSuccessStatusCode && (await response.Content.ReadAsStringAsync()) == "null"))
                    {
                        AppLogger.LogInfo($"Firebase document path '{fullPath}' does not exist yet.");
                        return true;
                    }
                    else if (!response.IsSuccessStatusCode)
                    {
                        AppLogger.LogError($"Firebase path check failed: {response.StatusCode}");
                        return false;
                    }

                    AppLogger.LogInfo($"Firebase document path '{fullPath}' exists and is accessible");
                    return true;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Firebase document path test failed", ex);
                return false;
            }
        }

        public void Dispose()
        {
            StopListening();
            firebaseClient?.Dispose();
            AppLogger.LogInfo("FirebaseManager disposed");
        }

        public string BuildDocumentPath()
        {
            if (!IsTotemConfigurationValid())
            {
                throw new InvalidOperationException("Configuration is not valid for building Firebase path");
            }

            string countryCode = MapCountryToCode(currentTotemConfig.Country);
            string path = $"{databaseDocumentParentPath}/{countryCode}/{currentTotemConfig.Business.ToLower()}/{currentTotemConfig.StoreId}/{currentTotemConfig.IdTotem}";
            AppLogger.LogInfo($"Built Firebase document path: {path}");
            return path;
        }

        public ConfigurationData GetCurrentConfiguration()
        {
            return currentTotemConfig;
        }

        public bool IsListening => isListening;
        #endregion

        #region Private Methods
        private void HandleFirebaseEvent(FirebaseEvent<Dictionary<string, object>> firebaseEvent)
        {
            try
            {
                // Log ALL events for debugging
                AppLogger.LogFirebaseEvent("FIREBASE_EVENT",
                    $"Event Type: {firebaseEvent.EventType}, Key: {firebaseEvent.Key}, " +
                    $"Object is null: {firebaseEvent.Object == null}");

                if (firebaseEvent?.Object != null)
                {
                    string key = firebaseEvent.Key;
                    var data = firebaseEvent.Object;

                    AppLogger.LogFirebaseEvent($"FIREBASE_{firebaseEvent.EventType}",
                        $"Key={key}, Data={JsonConvert.SerializeObject(data)}");

                    // Handle different event types
                    switch (firebaseEvent.EventType)
                    {
                        case FirebaseEventType.InsertOrUpdate:
                            AppLogger.LogFirebaseEvent("NEW_TICKET",
                                $"New ticket detected: Key={key}, Data={JsonConvert.SerializeObject(data)}");
                            // TODO: Process, map and print ticket
                            break;

                        case FirebaseEventType.Delete:
                            AppLogger.LogFirebaseEvent("TICKET_DELETED", $"Ticket deleted: Key={key}");
                            break;
                    }
                }
                else if (firebaseEvent != null)
                {
                    // Initial event might have null object
                    AppLogger.LogFirebaseEvent("FIREBASE_INITIAL",
                        $"Initial event or empty path - EventType: {firebaseEvent.EventType}, Key: {firebaseEvent.Key}");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Error handling Firebase event", ex);
            }
        }

        private void HandleFirebaseError(Exception error)
        {
            string errorMessage = "Firebase listener error";

            if (error is Firebase.Database.FirebaseException fbEx &&
                fbEx.InnerException is HttpRequestException httpEx)
            {
                if (httpEx.Message.Contains("401"))
                    errorMessage = "Firebase authentication error: Invalid API key or insufficient permissions";
                else if (httpEx.Message.Contains("403"))
                    errorMessage = "Firebase authorization error: Insufficient permissions to access this path";
                else if (httpEx.Message.Contains("404"))
                    errorMessage = "Firebase path does not exist yet";
                else
                    errorMessage = $"Firebase HTTP error: {httpEx.Message}";
            }

            AppLogger.LogError(errorMessage, error);
            isListening = false;
            currentSubscription?.Dispose();
        }

        private async Task<bool> CheckPathExistsAsync(string path)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var testUrl = $"{firebaseUrl}/{path}.json?auth={apiKey}";
                    var response = await httpClient.GetAsync(testUrl);

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return false;

                    if (!response.IsSuccessStatusCode)
                        return false;

                    var content = await response.Content.ReadAsStringAsync();
                    return content != "null";
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task CreatePathPlaceholderAsync(string path)
        {
            try
            {
                AppLogger.LogInfo($"Creating placeholder for path: {path}");

                var placeholder = new Dictionary<string, object>
                {
                    ["_placeholder"] = new
                    {
                        created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                        info = "Path created automatically to enable Firebase listener"
                    }
                };

                await firebaseClient.Child(path).PutAsync(placeholder);
                AppLogger.LogInfo("Path placeholder created successfully");
            }
            catch (Exception ex)
            {
                AppLogger.LogWarning($"Could not create path placeholder: {ex.Message}");
            }
        }

        private bool IsTotemConfigurationValid()
        {
            if (currentTotemConfig == null) return false;

            bool isValid = !string.IsNullOrWhiteSpace(currentTotemConfig.IdTotem) &&
                          !string.IsNullOrWhiteSpace(currentTotemConfig.Country) &&
                          !string.IsNullOrWhiteSpace(currentTotemConfig.Business) &&
                          !string.IsNullOrWhiteSpace(currentTotemConfig.StoreId);

            if (!isValid)
            {
                AppLogger.LogError("Totem configuration is invalid or incomplete");
            }

            return isValid;
        }

        private bool ConfigurationEquals(ConfigurationData config1, ConfigurationData config2)
        {
            if (config1 == null || config2 == null) return false;

            return config1.IdTotem == config2.IdTotem &&
                   config1.Country == config2.Country &&
                   config1.Business == config2.Business &&
                   config1.StoreId == config2.StoreId;
        }

        private string MapCountryToCode(string country)
        {
            if (string.IsNullOrEmpty(country))
                return "unknown";

            string normalizedCountry = country.Trim().ToLower();

            switch (normalizedCountry)
            {
                case "chile": return "cl";
                case "peru":
                case "perú": return "pe";
                case "colombia": return "co";
                default:
                    if (normalizedCountry == "cl" || normalizedCountry == "pe" || normalizedCountry == "co")
                        return normalizedCountry;

                    return normalizedCountry;
            }
        }
        #endregion
    }
}