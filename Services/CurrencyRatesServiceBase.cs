using eBanking.Interfaces;
using System.Text.Json;

namespace eBanking.Services
{
    public abstract class CurrencyRatesServiceBase<TApiResponse> : ICurrencyRateService
    {
        protected abstract string ApiUri { get; }
        protected abstract string ApiKey { get; }
        protected virtual int ThrottleMinutes => 1;

        protected Dictionary<string, double>? ApiResponse;
        protected DateTime ApiCallTimestamp;

        /// <summary>
        /// Processes API response and updates internal dictionary
        /// </summary>
        protected abstract void ProcessApiResponse(TApiResponse? apiResponse);

        /// <summary>
        /// Calls API, retrieves exchange rates and updates internal dictionary
        /// </summary>
        public async Task CallApi(IHttpClientFactory httpClientFactory)
        {
            if ((DateTime.Now - ApiCallTimestamp).TotalMinutes < ThrottleMinutes)
                return;

            ApiCallTimestamp = DateTime.Now;

            HttpClient client = httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync($"{ApiUri}?access_key={ApiKey}");
            string responseBody = await response.Content.ReadAsStringAsync();

            try
            {
                var apiResponse = JsonSerializer.Deserialize<TApiResponse>(responseBody);
                ProcessApiResponse(apiResponse);
            }
            catch
            {

            }
        }

        /// <summary>
        /// Gets exchange rate for currency from internal dictionary, falls back to 1
        /// </summary>
        public virtual double GetRate(string currencyName) =>
            ApiResponse?.TryGetValue(currencyName, out double rate) == true ? rate : 1;

        /// <summary>
        /// Gets all exchange rates from internal dictionary, falls back to empty collection
        /// </summary>
        public virtual Dictionary<string, double> GetRates() =>
            ApiResponse ?? [];
    }
}
