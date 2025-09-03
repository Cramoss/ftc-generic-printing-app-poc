using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System.Configuration;

namespace FTC_Generic_Printing_App_POC
{
    public class FirebaseManager
    {
        private FirebaseClient firebaseClient;
        private readonly string firebaseUrl;
        private readonly string projectId;
        private readonly string apiKey;
        private readonly string documentPath;
        private ConfigurationData currentConfig;
        private bool isListening = false;
        private IDisposable currentSubscription;

        public FirebaseManager()
        {
            firebaseUrl = System.Configuration.ConfigurationManager.AppSettings["Firebase_DatabaseUrl"] ??
                throw new InvalidOperationException("Firebase_DatabaseUrl not configured");

            projectId = System.Configuration.ConfigurationManager.AppSettings["Firebase_ProjectId"] ??
                throw new InvalidOperationException("Firebase_ProjectId not configured");

            apiKey = System.Configuration.ConfigurationManager.AppSettings["Firebase_ApiKey"] ??
                throw new InvalidOperationException("Firebase_ApiKey not configured");

            documentPath = System.Configuration.ConfigurationManager.AppSettings["Firebase_DocumentPath"] ??
                throw new InvalidOperationException("Firebase_DocumentPath not configured");

            LoadCurrentConfiguration();
            InitializeFirebase();
            AppLogger.LogInfo($"FirebaseManager initialized for project: {projectId}");
        }

        private void LoadCurrentConfiguration()
        {
            try
            {
                currentConfig = FTC_Generic_Printing_App_POC.ConfigurationManager.LoadConfiguration();
                AppLogger.LogInfo($"Loaded current configuration. Totem: {currentConfig.IdTotem}, Store: {currentConfig.StoreId}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading current configuration", ex);
                currentConfig = new ConfigurationData();
            }
        }

        public void RefreshConfiguration()
        {
            try
            {
                var previousConfig = currentConfig;
                LoadCurrentConfiguration();

                if (isListening && !ConfigurationEquals(previousConfig, currentConfig))
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

        private bool ConfigurationEquals(ConfigurationData config1, ConfigurationData config2)
        {
            if (config1 == null || config2 == null) return false;
            return config1.IdTotem == config2.IdTotem &&
                   config1.Country == config2.Country &&
                   config1.Business == config2.Business &&
                   config1.StoreId == config2.StoreId;
        }

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

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                AppLogger.LogInfo("Testing Firebase connection...");

                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var testUrl = $"{firebaseUrl}connection-test.json?auth={apiKey}";
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

                var testPath = $"connection-test-{DateTime.Now.Ticks}";
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

        public string BuildDocumentPath()
        {
            try
            {
                if (!IsConfigurationValid())
                {
                    throw new InvalidOperationException("Configuration is not valid for building Firebase path");
                }

                string path = $"{documentPath}/{currentConfig.Country.ToLower()}/{currentConfig.Business.ToLower()}/{currentConfig.StoreId}/{currentConfig.IdTotem}";
                AppLogger.LogInfo($"Built Firebase document path: {path}");
                return path;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error building Firebase document path", ex);
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
                    throw new InvalidOperationException("Cannot start Firebase listener. Configuration is invalid");
                }

                string fullPath = BuildDocumentPath();
                AppLogger.LogInfo($"Starting Firebase listener on path: {fullPath}");

                var observable = firebaseClient
                    .Child(fullPath)
                    .AsObservable<Dictionary<string, object>>();

                // TODO: Implement listener call on new ticket 

                //currentSubscription = observable.Subscribe(
                //    onNext: (firebaseEvent) =>
                //    {
                //        OnFirebaseDataChanged(firebaseEvent);
                //    },
                //    onError: (error) =>
                //    {
                //        AppLogger.LogError("Firebase listener error", error);
                //        isListening = false;
                //        currentSubscription?.Dispose();
                //    }
                //);

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
      
        private bool IsConfigurationValid()
        {
            return currentConfig != null &&
                   !string.IsNullOrWhiteSpace(currentConfig.IdTotem) &&
                   !string.IsNullOrWhiteSpace(currentConfig.Country) &&
                   !string.IsNullOrWhiteSpace(currentConfig.Business) &&
                   !string.IsNullOrWhiteSpace(currentConfig.StoreId);
        }

        public bool IsListening => isListening;

        public ConfigurationData GetCurrentConfiguration()
        {
            return currentConfig;
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
    }
}