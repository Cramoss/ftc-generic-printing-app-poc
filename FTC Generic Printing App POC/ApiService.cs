using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FTC_Generic_Printing_App_POC
{
    public class ApiService
    {
        private static readonly HttpClient httpClient = new HttpClient();

        // TODO: Handle and encrypt these hardcoded values.
        private const string AUTH_URL = "https://api-prod-ftc-sc.falabella.com/stro-dgsp-store-info/v1/authorization";
        private const string STORES_URL = "https://api-prod-ftc-sc.falabella.com/stro-dgsp-store-info/v1/api/v1/stores";
        private const string CLIENT_ID = "test";
        private const string CLIENT_SECRET = "test";

        public async Task<string> GetAccessTokenAsync()
        {
            try
            {
                AppLogger.LogInfo("Requesting access token from Stores API");

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

                AppLogger.LogInfo($"Fetching stores for {xEcommName}");

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
        public string nodeId { get; set; }
        public string name { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string regionName { get; set; }
        public string country { get; set; }
        public string buName { get; set; }
        public string isActive { get; set; }

        // Override to display store name in configuration ComboBox
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