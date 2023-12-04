using eBanking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eBanking.Areas.Identity.Data;

public class BankDbContext : IdentityDbContext<BankUser, BankRole, string,
        IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public BankDbContext(DbContextOptions<BankDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BankUser>()
            .HasOne(user => user.Role)
            .WithMany(role => role.Users)
            .OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<BankRole> BankRole { get; set; } = default!;
    public DbSet<CurrencyModel> Currencies { get; set; } = default!;
    public DbSet<AccountModel> Accounts { get; set; } = default!;
}
