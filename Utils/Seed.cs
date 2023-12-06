using eBanking.Areas.Identity.Data;
using eBanking.Models;
using Microsoft.AspNetCore.Identity;

namespace eBanking.Utils
{
    public static class Seed
    {
        /// <summary>
        /// Seeds initial data for BankDbContext including roles, employee and currency exchange rates
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public static void SeedData(BankDbContext context, UserManager<BankUser> userManager, RoleManager<BankRole> roleManager)
        {
            if (context.Users.Any()) return;

            // Seed roles
            SeedRole(roleManager, "0", "Employee").Wait();
            SeedRole(roleManager, "1", "Customer").Wait();

            // Seed employee
            SeedEmployee(context, userManager, "Nikos", "Daridis", "Address 123", 1234567891, "daridis@email.com", 123456789, "Pass123").Wait();

            // Seed currencies
            SeedCurrency(context, "AUD", 1.63);
            SeedCurrency(context, "CHF", 0.95);
            SeedCurrency(context, "GBP", 0.86);
            SeedCurrency(context, "USD", 1.09);

            // Method to seed a role
            static async Task SeedRole(RoleManager<BankRole> roleManager, string id, string name)
            {
                BankRole role = new BankRole();
                role.Id = id;
                role.Name = name;
                role.NormalizedName = name.ToUpper();
                role.ConcurrencyStamp = "BankRole";
                await roleManager.CreateAsync(role);
            }

            // Method to seed an employee
            static async Task SeedEmployee(BankDbContext context, UserManager<BankUser> userManager, string firstName, string lastName, string address, int phone, string email, int afm, string password)
            {
                BankUser user = new BankUser();
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Address = address;
                user.Phone = phone;
                user.Email = email;
                user.UserName = user.Email;
                user.AFM = afm;
                user.RoleId = context.Roles.Where(role => role.Name == "Employee").Select(role => role.Id).SingleOrDefault();
                user.SecurityStamp = Guid.NewGuid().ToString();
                await userManager.CreateAsync(user, password);
                string? userId = await userManager.GetUserIdAsync(user);
                BankUser? newUser = context.Users.Where(user => user.Id == userId).FirstOrDefault();
                await userManager.AddToRoleAsync(newUser!, "Employee");
            }

            // Method to seed a currency
            static void SeedCurrency(BankDbContext context, string name, double price)
            {
                CurrencyModel currency = new CurrencyModel();
                currency.Name = name;
                currency.Price = price;
                context.Currencies.Add(currency);
                context.SaveChanges();
            }
        }
    }
}
