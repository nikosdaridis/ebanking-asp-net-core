using eBanking.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBanking.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CurrencyController(BankDbContext context) : ControllerBase
    {
        /// <summary>
        /// Gets exchange rates for all currencies
        /// </summary>
        [HttpGet]
        public async Task<Dictionary<string, double>> GetRates() =>
            await context.Currencies.ToDictionaryAsync(currency => currency.Name, currency => currency.Price);
    }
}
