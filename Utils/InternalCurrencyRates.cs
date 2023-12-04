using Newtonsoft.Json;

namespace eBanking.Utils
{
    public static class InternalCurrencyRates
    {
        private static Dictionary<string, double>? _apiResponse;

        /// <summary>
        /// Calls Bank API to retrieve exchange rates and updates the internal dictionary
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public static async Task CallApi(IHttpClientFactory httpClientFactory)
        {
            HttpClient? client = httpClientFactory.CreateClient();
            HttpResponseMessage? response = await client.GetAsync("https://localhost:7110/Currency");
            string responseBody = await response.Content.ReadAsStringAsync();

            _apiResponse = JsonConvert.DeserializeObject<Dictionary<string, double>>(responseBody);
        }

        /// <summary>
        /// Gets the exchange rate for a specified currency from internal dictionary
        /// </summary>
        /// <param name="currencyName"></param>
        /// <returns>The exchange rate for the specified currency or default rate of 1 if data is not available</returns>
        public static double GetRate(string currencyName)
        {
            return _apiResponse?[currencyName] ?? 1;
        }

        /// <summary>
        /// Gets all exchange rates from internal dictionary
        /// </summary>
        /// <returns>A dictionary containing all exchange rates or empty dictionary if data is not available</returns>
        public static Dictionary<string, double> GetRates()
        {
            return _apiResponse ?? [];
        }
    }
}
