using eBanking.Areas.Identity.Data;
using eBanking.Services;
using eBanking.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eBanking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

            // Retrieve the connection string from configuration
            string? connectionString = builder.Configuration.GetConnectionString("BankDbContextConnection") ?? throw new InvalidOperationException("Connection string 'BankDbContextConnection' not found.");

            // Add DbContext to the services
            builder.Services.AddDbContext<BankDbContext>(options => options.UseSqlServer(connectionString));

            // Configure Identity options and add Identity services to the container
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

            // Add services to the container
            builder.Services.AddHttpClient();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddControllersWithViews();


            WebApplication? app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Seed initial data
            using (IServiceScope? scope = app.Services.CreateScope())
            {
                IServiceProvider? services = scope.ServiceProvider;
                BankDbContext? context = services.GetRequiredService<BankDbContext>();
                UserManager<BankUser>? userManager = services.GetRequiredService<UserManager<BankUser>>();
                RoleManager<BankRole>? roleManager = services.GetRequiredService<RoleManager<BankRole>>();

                Seed.SeedData(context, userManager, roleManager);
            }

            app.Run();
        }
    }
}
