using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FTC_Generic_Printing_App_POC
{
    public class StoresApiService
    {
        #region Fields
        private static readonly HttpClient httpClient = new HttpClient();

        private string AUTH_URL;
        private string STORES_URL;
        private string CLIENT_ID;
        private string CLIENT_SECRET;
        #endregion

        #region Initialization
        public StoresApiService()
        {
            var config = ConfigurationManager.LoadStoresApiConfiguration();

            AUTH_URL = config.AuthUrl;
            STORES_URL = config.StoresUrl;
            CLIENT_ID = config.ClientId;
            CLIENT_SECRET = config.ClientSecret;
            AppLogger.LogInfo("ApiService initialized with configuration values");
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

            return value ?? string.Empty;
        }

        public void ReloadConfiguration()
        {
            try
            {
                AppLogger.LogInfo("Reloading API service configuration");
                var config = ConfigurationManager.LoadStoresApiConfiguration();

                AUTH_URL = config.AuthUrl;
                STORES_URL = config.StoresUrl;
                CLIENT_ID = config.ClientId;
                CLIENT_SECRET = config.ClientSecret;

                AppLogger.LogInfo("API service configuration reloaded successfully");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Failed to reload API service configuration", ex);
            }
        }

        public async Task<string> GetAccessTokenAsync()
        {
            try
            {
                AppLogger.LogInfo($"Requesting access token from Stores API. Using URL: {AUTH_URL}");

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", CLIENT_ID),
                    new KeyValuePair<string, string>("client_secret", CLIENT_SECRET),
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                });

                var response = await httpClient.PostAsync(AUTH_URL, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseBody);
                    AppLogger.LogInfo("Access token obtained successfully");
                    return authResponse.access_token;
                }
                else
                {
                    AppLogger.LogError($"Auth Stores API call failed: {response.StatusCode} - {responseBody}");
                    throw new Exception($"Stores API Authentication call failed: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error getting access token", ex);
                throw;
            }
        }

        public async Task<List<Store>> GetStoresAsync(string country, string business)
        {
            try
            {
                string accessToken = await GetAccessTokenAsync();
                string xEcommName = BuildEcommName(country, business);

                AppLogger.LogInfo($"Fetching stores for {xEcommName} at URL: {STORES_URL}");

                var request = new HttpRequestMessage(HttpMethod.Get, STORES_URL);
                request.Headers.Add("x-environment", "PROD");
                request.Headers.Add("x-ecomm-name", xEcommName);
                request.Headers.Add("Authorization", $"Bearer {accessToken}");

                var response = await httpClient.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var stores = JsonConvert.DeserializeObject<List<Store>>(responseBody);
                    AppLogger.LogInfo($"Retrieved {stores?.Count ?? 0} stores successfully");
                    return stores ?? new List<Store>();
                }
                else
                {
                    AppLogger.LogError($"Stores API call failed: {response.StatusCode} - {responseBody}");
                    throw new Exception($"Failed to fetch stores: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error fetching stores", ex);
                throw;
            }
        }
        #endregion

        #region Helper Methods
        private string BuildEcommName(string country, string business)
        {
            string countryCode = "";
            switch (country.ToLower())
            {
                case "chile":
                    countryCode = "cl";
                    break;
                case "peru":
                    countryCode = "pe";
                    break;
                case "colombia":
                    countryCode = "co";
                    break;
                default:
                    countryCode = "cl";
                    break;
            }

            string businessCode = "";
            switch (business.ToLower())
            {
                case "falabella":
                    businessCode = "falabella";
                    break;
                case "tottus":
                    businessCode = "tottus";
                    break;
                case "sodimac":
                    businessCode = "sodimac";
                    break;
                default:
                    businessCode = "falabella";
                    break;
            }

            return $"{businessCode}-{countryCode}";
        }
        #endregion
    }

    public class AuthResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

    public class Store
    {
        public string id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string buName { get; set; }
        public override string ToString()
        {
            return name;
        }
    }

    public class ConfigurationData
    {
        public string IdTotem { get; set; } = "";
        public string Country { get; set; } = "";
        public string Business { get; set; } = "";
        public string Store { get; set; } = "";
        public string StoreId { get; set; } = "";
    }
}