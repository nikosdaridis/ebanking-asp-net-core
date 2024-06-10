namespace eBanking.Interfaces
{
    public interface ICurrencyRateService
    {
        Task CallApi(IHttpClientFactory httpClientFactory);
        double GetRate(string currencyName);
        Dictionary<string, double> GetRates();
    }
}
