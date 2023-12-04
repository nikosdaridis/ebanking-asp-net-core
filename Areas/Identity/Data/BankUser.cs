using eBanking.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBanking.Areas.Identity.Data;

public class BankUser : IdentityUser
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Address { get; set; } = "";

    public long Phone { get; set; }

    public int AFM { get; set; }

    [ForeignKey("Role")]
    public string? RoleId { get; set; } = "";

    public virtual BankRole? Role { get; set; }

    public virtual ICollection<AccountModel>? Accounts { get; set; } = null;
}