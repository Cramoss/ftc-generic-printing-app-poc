using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Linq;
using FTC_Generic_Printing_App_POC.Services;
using Newtonsoft.Json.Linq;

namespace FTC_Generic_Printing_App_POC
{
    public class FirebaseService : IDisposable
    {
        #region Fields
        private IFirebaseClient firebaseClient;
        private string firebaseUrl;
        private string projectId;
        private string apiKey;
        private string databaseDocumentParentPath;
        private readonly string databaseDocumentTestingPath = "connection-test";
        private ConfigurationData currentTotemConfig;
        private bool isListening = false;
        private EventStreamResponse currentEventStream;
        private CancellationTokenSource cancellationTokenSource;
        private readonly HashSet<string> processedDocumentIds = new HashSet<string>();
        private readonly object lockObject = new object();
        private PrinterService printerService;
        #endregion

        #region Initialization
        public FirebaseService()
        {
            LoadFirebaseSettings();
            LoadCurrentConfiguration();
            InitializeFirebase();
            printerService = new PrinterService();
            AppLogger.LogInfo($"Firebase initialized for project: {projectId}");
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
                IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
                {
                    BasePath = firebaseUrl,
                };

                firebaseClient = new FirebaseClient(config);
                AppLogger.LogInfo($"FireSharp client initialized");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error initializing FireSharp client", ex);
                throw;
            }
        }
        #endregion

        #region Core Methods
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

        public void ReloadTotemConfiguration(bool autoStartListener = false)
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
                        AppLogger.LogInfo("Configuration changed. Stopping Firebase listener");
                        DisconnectListener();
                    }

                    if (autoStartListener && IsTotemConfigurationValid())
                    {
                        AppLogger.LogInfo("Starting Firebase listener with new configuration");
                        Task.Run(() => ConnectListenerAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error reloading totem configuration", ex);
            }
        }

        public async Task ConnectListenerAsync()
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
                AppLogger.LogInfo($"Starting FireSharp listener on path: {fullPath}");

                cancellationTokenSource = new CancellationTokenSource();

                await Task.Run(async () =>
                {
                    try
                    {
                        currentEventStream = await firebaseClient.OnAsync(fullPath,
                            added: (sender, args, context) => HandleValueAdded(args, (string)context));

                        isListening = true;
                        AppLogger.LogFirebaseEvent("LISTENER_STARTED", $"FireSharp listening on path: {fullPath}");

                        // Keep the listener alive
                        while (!cancellationTokenSource.Token.IsCancellationRequested)
                        {
                            try
                            {
                                await Task.Delay(1000, cancellationTokenSource.Token);
                            }
                            catch (TaskCanceledException)
                            {
                                break;
                            }
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        AppLogger.LogInfo("Firebase listener task was canceled");
                    }
                    catch (Exception ex)
                    {
                        AppLogger.LogError("Error in FireSharp listener", ex);
                        isListening = false;
                    }
                });
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error starting FireSharp listener", ex);
                isListening = false;
                throw;
            }
        }

        public void DisconnectListener()
        {
            try
            {
                if (!isListening)
                {
                    AppLogger.LogInfo("Firebase listener not active");
                    return;
                }

                isListening = false;

                try
                {
                    cancellationTokenSource?.Cancel();
                    currentEventStream?.Dispose();
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("Error while canceling listener", ex);
                }
                finally
                {
                    currentEventStream = null;
                    cancellationTokenSource = null;
                }

                // Clear the processed document IDs when stopping the listener
                lock (lockObject)
                {
                    processedDocumentIds.Clear();
                }

                AppLogger.LogFirebaseEvent("LISTENER_STOPPED", "FireSharp listener stopped");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error stopping FireSharp listener", ex);
            }
        }

        public void Dispose()
        {
            DisconnectListener();
            printerService?.Dispose();
            AppLogger.LogInfo("FirebaseManager disposed");
        }

        private void HandleValueAdded(FireSharp.EventStreaming.ValueAddedEventArgs args, string context)
        {
            try
            {
                string pathParts = args.Path.TrimStart('/');
                string[] pathSegments = pathParts.Split('/');
                string rootId = pathSegments.FirstOrDefault();

                if (string.IsNullOrEmpty(rootId))
                {
                    AppLogger.LogWarning("Skipping new entry due to invalid path format. Skipping");
                    return;
                }

                // Since the FireSharp library doesn't provide a method to just detect single new entries,
                // the added event may trigger multiple times for the same current document.
                // For this, we need to check if we've already processed this document in the current session.
                bool isNewDocument;
                lock (lockObject)
                {
                    isNewDocument = processedDocumentIds.Add(rootId);
                }

                if (!isNewDocument)
                {
                    return;
                }

                Task.Run(async () =>
                {
                    try
                    {
                        string basePath = BuildDocumentPath();
                        string documentPath = $"{basePath}/{rootId}";

                        var documentResponse = await firebaseClient.GetAsync(documentPath);
                        if (documentResponse == null || documentResponse.Body == "null" || string.IsNullOrEmpty(documentResponse.Body))
                        {
                            AppLogger.LogWarning("Document is incomplete. Skipping");
                            return;
                        }

                        var documentJson = JObject.Parse(documentResponse.Body);

                        // Check if document has been processed
                        if (documentJson["readed"] != null && documentJson["readed"].Value<bool>())
                        {
                            return;
                        }

                        // Check if document has required fields
                        if (documentJson["data"] != null && documentJson["id_plantilla"] != null)
                        {
                            await ProcessDocument(rootId, documentJson);
                            AppLogger.LogInfo($"Finished document {rootId} processing");
                        }
                    }
                    catch (Exception ex)
                    {
                        AppLogger.LogError($"Error checking document status: {ex.Message}", ex);
                    }
                });
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Error handling value added event", ex);
            }
        }

        private async Task ProcessDocument(string rootId, JObject document)
        {
            try
            {
                AppLogger.LogFirebaseEvent("NEW_DOCUMENT",
                    $"Processing new document with ID: {rootId}");

                await printerService.PrintDocumentAsync(document);
                AppLogger.LogInfo($"Document {rootId} sent to printer successfully");

                await MarkDocumentAsRead(rootId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Error processing document", ex);
            }
        }

        private async Task MarkDocumentAsRead(string rootId)
        {
            try
            {
                string basePath = BuildDocumentPath();
                string readPath = $"{basePath}/{rootId}/readed";

                AppLogger.LogInfo($"Marking document {rootId} as read");

                var response = await firebaseClient.SetAsync(readPath, true);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    AppLogger.LogInfo($"Successfully marked document {rootId} as read");
                }
                else
                {
                    AppLogger.LogWarning($"Failed to mark document {rootId} as read. Status: {response?.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Error marking document as read", ex);
            }
        }

        public bool IsListening => isListening;
        #endregion

        #region Helper Methods
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

        #region Test methods
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

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                AppLogger.LogInfo("Testing FireSharp connection...");

                var testPath = $"{databaseDocumentTestingPath}-{DateTime.Now.Ticks}";
                var testData = new { timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(), test = true };

                // Write
                var setResponse = await firebaseClient.SetAsync(testPath, testData);
                if (setResponse == null || setResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    AppLogger.LogError("FireSharp write test failed");
                    return false;
                }

                // Read
                var getResponse = await firebaseClient.GetAsync(testPath);
                if (getResponse == null || getResponse.Body == "null")
                {
                    AppLogger.LogError("FireSharp read test failed");
                    return false;
                }

                AppLogger.LogInfo("FireSharp connection test successful");
                return true;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("FireSharp connection test failed", ex);
                return false;
            }
        }

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
                AppLogger.LogInfo($"Testing FireSharp document path: {fullPath}");

                var response = await firebaseClient.GetAsync(fullPath);

                if (response == null)
                {
                    AppLogger.LogError("FireSharp path check failed: null response");
                    return false;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.Body == "null")
                {
                    AppLogger.LogInfo($"Firebase document path '{fullPath}' does not exist yet (this is OK)");
                    return true;
                }

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    AppLogger.LogError($"FireSharp path check failed: {response.StatusCode}");
                    return false;
                }

                AppLogger.LogInfo($"Firebase document path '{fullPath}' exists and is accessible");
                return true;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Firebase document path test failed", ex);
                return false;
            }
        }
        #endregion
    }
}