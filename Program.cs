using eBanking.Areas.Identity.Data;
using eBanking.Utils;
using Microsoft.AspNetCore.Identity;

namespace eBanking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

            ConfigureServices.AddServices(builder);

            WebApplication? app = builder.Build();

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
            using IServiceScope? scope = app.Services.CreateScope();
            IServiceProvider? services = scope.ServiceProvider;
            BankDbContext? context = services.GetRequiredService<BankDbContext>();
            UserManager<BankUser>? userManager = services.GetRequiredService<UserManager<BankUser>>();
            RoleManager<BankRole>? roleManager = services.GetRequiredService<RoleManager<BankRole>>();
            Seed.SeedData(context, userManager, roleManager);

            app.Run();
        }
    }
}
