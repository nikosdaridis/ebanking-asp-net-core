using AutoMapper;
using eBanking.Areas.Identity.Data;
using eBanking.Models;
using eBanking.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eBanking.Services
{
    public class EmployeeService(HelperService helperService, BankDbContext dbContext, UserManager<BankUser> userManager, IMapper mapper)
    {
        // GET: EmployeeController
        public IEnumerable<BankUserViewModel> Index() =>
            mapper.Map<IEnumerable<BankUserViewModel>>(dbContext.Users.Where(user => user.RoleId == "1"));

        // POST: EmployeeController/Create
        public async Task<string> Create(BankUserViewModel userInput, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return "Index";

            BankUser user = mapper.Map<BankUser>(userInput);
            user.Email = user.UserName;
            user.RoleId = "1";

            IdentityResult? result = await userManager.CreateAsync(user, userInput.Password);

            if (result.Succeeded)
                await userManager.AddToRoleAsync(user, "Customer");

            return "Index";
        }

        // POST: EmployeeController/AddAccount
        public string AddAccount(AccountViewModel accountInput)
        {
            if (helperService.FindEntityByProperty<BankUser>("AFM", accountInput.AFM) is null)
                return "CustomerNotFound";

            if (helperService.FindEntityByProperty<AccountModel>("Number", accountInput.Number) is not null)
                return "AccountAlreadyExists";

            AccountModel account = mapper.Map<AccountModel>(accountInput);

            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
            return "Index";
        }

        // POST: EmployeeController/DeleteCustomer
        public string DeleteCustomer(BankUserViewModel userInput)
        {
            BankUser? user = helperService.FindEntityByProperty<BankUser>("AFM", userInput.AFM);

            if (user is null)
                return "CustomerNotFound";

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            return "Index";
        }

        // POST: EmployeeController/DeleteAccount
        public string DeleteAccount(AccountViewModel accountInput)
        {
            AccountModel? account = helperService.FindEntityByProperty<AccountModel>("Number", accountInput.Number);

            if (account is null)
                return "AccountNotFound";

            dbContext.Accounts.Remove(account);
            dbContext.SaveChanges();
            return "Index";
        }

        // GET: EmployeeController/Edit/afm
        public BankUserViewModel? Edit(int afm) =>
            helperService.FindEntityByProperty<BankUser, BankUserViewModel>("AFM", afm);

        // POST: EmployeeController/Edit/afm
        public string Edit(BankUserViewModel userInput, int currentAFM)
        {
            BankUser user = helperService.FindEntityByProperty<BankUser>("AFM", currentAFM)!;

            user.FirstName = userInput.FirstName;
            user.LastName = userInput.LastName;
            user.Address = userInput.Address;
            user.Phone = userInput.Phone;
            user.UserName = userInput.UserName;
            user.Email = user.UserName;
            user.NormalizedUserName = user.UserName.ToUpper();
            user.NormalizedEmail = user.Email.ToUpper();

            dbContext.SaveChanges();
            return "Index";
        }

        // POST: EmployeeController/FindCustomer
        public int? FindCustomer(BankUserViewModel userInput) =>
            helperService.FindEntityByProperty<BankUser>("AFM", userInput.AFM)?.AFM;

        // GET: EmployeeController/FindCustomerDetails
        public BankUserViewModel FindCustomerDetails(int userAFM) =>
            helperService.FindEntityByProperty<BankUser, BankUserViewModel>("AFM", userAFM)!;

        // POST: EmployeeController/EditAccount
        public uint? EditAccount(AccountViewModel accountInput) =>
            helperService.FindEntityByProperty<AccountModel>("Number", accountInput.Number)?.Number;

        // GET: EmployeeController/EditAccountDetails/number
        public AccountViewModel EditAccountDetails(uint accountNumber) =>
            helperService.FindEntityByProperty<AccountModel, AccountViewModel>("Number", accountNumber)!;

        // POST: EmployeeController/EditAccountDetails/number
        public string EditAccountDetails(AccountViewModel accountInput, uint currentNumber)
        {
            AccountModel? account = helperService.FindEntityByProperty<AccountModel>("Number", currentNumber);

            if (account is null)
                return "AccountNotFound";

            if (accountInput.Number != currentNumber && helperService.FindEntityByProperty<AccountModel>("Number", accountInput.Number) is not null)
                return "AccountAlreadyExists";

            account!.Number = accountInput.Number;
            account.Branch = accountInput.Branch;
            account.Type = accountInput.Type;

            dbContext.SaveChanges();
            return "Index";
        }
    }
}
