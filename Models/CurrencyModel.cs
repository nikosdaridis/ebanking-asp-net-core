using System.ComponentModel.DataAnnotations;

namespace eBanking.Models
{
    public class CurrencyModel
    {
        [Key]
        public string? Name { get; set; }
        public double Price { get; set; }
    }
}
