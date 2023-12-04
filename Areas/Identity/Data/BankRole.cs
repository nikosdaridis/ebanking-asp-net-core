using Microsoft.AspNetCore.Identity;

namespace eBanking.Areas.Identity.Data
{
    public class BankRole : IdentityRole
    {
        public virtual ICollection<BankUser>? Users { get; set; }
    }
}
