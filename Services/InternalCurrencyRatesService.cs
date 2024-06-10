namespace eBanking.Services
{
    public class InternalCurrencyRatesService : CurrencyRatesServiceBase<Dictionary<string, double>>
    {
        protected override string ApiUri => "https://localhost:7110/Currency";
        protected override string ApiKey => "";

        protected override void ProcessApiResponse(Dictionary<string, double>? apiResponse) =>
            ApiResponse = apiResponse;
    }
}
