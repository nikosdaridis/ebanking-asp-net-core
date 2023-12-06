using System.ComponentModel.DataAnnotations;

namespace eBanking.Models
{
    public class AccountModel
    {
        [Key]
        public int Id { get; set; }

        public int AFM { get; set; }

        public double Balance { get; set; }

        public uint Number { get; set; }

        public string Branch { get; set; } = "";

        public string Type { get; set; } = "";
    }
}
