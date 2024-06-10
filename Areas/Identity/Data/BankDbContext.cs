using eBanking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eBanking.Areas.Identity.Data;

public class BankDbContext(DbContextOptions<BankDbContext> options) : IdentityDbContext<BankUser, BankRole, string,
        IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>(options)
{
    public DbSet<BankRole> BankRole { get; set; }
    public DbSet<CurrencyModel> Currencies { get; set; }
    public DbSet<AccountModel> Accounts { get; set; }

    /// <summary>
    /// Configures database schema
    /// </summary>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BankUser>()
            .HasOne(user => user.Role)
            .WithMany(role => role.Users)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
