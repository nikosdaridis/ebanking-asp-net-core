using eBanking.Areas.Identity.Data;
using eBanking.Interfaces;
using eBanking.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eBanking
{
    public class ConfigureServices
    {
        /// <summary>
        /// Adds services
        /// </summary>
        public static void AddServices(WebApplicationBuilder builder)
        {
            string connectionString = builder.Configuration.GetConnectionString("BankDbContextConnection")
                ?? throw new InvalidOperationException("Connection string BankDbContextConnection not found");

            builder.Services.AddDbContext<BankDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<BankUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddRoles<BankRole>()
                .AddRoleManager<RoleManager<BankRole>>()
                .AddUserManager<UserManager<BankUser>>()
                .AddEntityFrameworkStores<BankDbContext>();

            builder.Services.AddHttpClient();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddSingleton<InternalCurrencyRatesService>();
            builder.Services.AddSingleton<ExternalCurrencyRatesService>();

            builder.Services.AddScoped<HelperService>();
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<EmployeeService>();

            builder.Services.AddControllersWithViews();
        }
    }
}
