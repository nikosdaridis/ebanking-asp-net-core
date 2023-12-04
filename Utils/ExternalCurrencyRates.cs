using Newtonsoft.Json;

namespace eBanking.Utils
{
    public static class ExternalCurrencyRates
    {
        private const string API_KEY = "273f245b5b0b31c0060a45d83e7b1b28";
        private const int THROTTLE_MINUTES = 1;

        private static ApiResponse? _apiResponse;
        private static DateTime _apiCallTimestamp;

        /// <summary>
        /// Represents the response from fixer.io API
        /// </summary>
        private class ApiResponse
        {
            [JsonProperty("success")]
            bool Success { get; set; }

            [JsonProperty("timestamp")]
            long Timestamp { get; set; }

            [JsonProperty("base")]
            string? Base { get; set; }

            [JsonProperty("date")]
            string? Date { get; set; }

            [JsonProperty("rates")]
            public Dictionary<string, double>? Rates { get; set; }
        }

        /// <summary>
        /// Calls the fixer.io API to retrieve the latest exchange rates and updates the internal API response
        /// The API call is throttled to an interval of 1 minute
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public static async Task CallApi(IHttpClientFactory httpClientFactory)
        {
            if ((DateTime.Now - _apiCallTimestamp).TotalMinutes < THROTTLE_MINUTES) return;

            _apiCallTimestamp = DateTime.Now;

            HttpClient? client = httpClientFactory.CreateClient();
            HttpResponseMessage? response = await client.GetAsync($"http://data.fixer.io/api/latest?access_key={API_KEY}&base=EUR");
            string responseBody = await response.Content.ReadAsStringAsync();

            _apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
        }

        /// <summary>
        /// Gets the exchange rate for a specified currency from internal API response
        /// </summary>
        /// <param name="currencyName"></param>
        /// <returns>
        /// The exchange rate for the specified currency. If the rate is not available from the fixer.io API response
        /// fall back to the internal Bank currency rates. If the currency is still not available, return default rate of 1
        /// </returns>
        public static double GetRate(string currencyName)
        {
            return _apiResponse?.Rates?[currencyName] ?? InternalCurrencyRates.GetRate(currencyName);
        }
    }
}
