using eBanking.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBanking.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly BankDbContext _context;

        public CurrencyController(BankDbContext context)
        {
            _context = context;
        }

        // GET: Currency
        [HttpGet]
        public async Task<Dictionary<string, double>> GetRates()
        {
            return await _context.Currencies.ToDictionaryAsync(currency => currency.Name, currency => currency.Price);
        }
    }
}