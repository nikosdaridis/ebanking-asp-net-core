using eBanking.Models.API;

namespace eBanking.Services
{
    public class ExternalCurrencyRatesService(InternalCurrencyRatesService internalCurrencyRatesService) : CurrencyRatesServiceBase<FixerioResponse>
    {
        protected override string ApiUri => "http://data.fixer.io/api/latest";
        protected override string ApiKey => "273f245b5b0b31c0060a45d83e7b1b28";

        protected override void ProcessApiResponse(FixerioResponse? apiResponse)
        {
            if (apiResponse is not null && apiResponse.Success)
                ApiResponse = apiResponse.Rates;
        }

        public override double GetRate(string currencyName) =>
           ApiResponse?.TryGetValue(currencyName, out double rate) == true
               ? rate
               : internalCurrencyRatesService.GetRate(currencyName);
    }
}
