using AutoMapper;
using eBanking.Areas.Identity.Data;
using eBanking.Models;
using eBanking.ViewModels;

namespace eBanking.Services
{
    public class CustomerService(HelperService helperService, BankDbContext dbContext, IMapper mapper)
    {
        // GET: CustomerController
        public IEnumerable<AccountViewModel> Index(string? userId)
        {
            if (userId is null)
                return [];

            BankUserViewModel? user = helperService.FindEntityByProperty<BankUser, BankUserViewModel>("Id", userId);

            if (user is null)
                return [];

            return mapper.Map<IEnumerable<AccountViewModel>>(dbContext.Accounts.Where(account => account.AFM == user.AFM));
        }

        // GET: CustomerController/Deposit
        public bool Deposit(uint number) =>
            helperService.EntityExists<AccountModel>(number);

        // POST: CustomerController/Deposit
        public string Deposit(AccountViewModel accountInput)
        {
            AccountModel? account = helperService.FindEntityByProperty<AccountModel>("Number", accountInput.Number);

            if (account is null)
                return "AccountNotFound";

            account.Balance += accountInput.Balance;
            dbContext.SaveChanges();
            return "Index";
        }

        // GET: CustomerController/Transfer
        public AccountViewModel? Transfer(uint number) =>
            helperService.FindEntityByProperty<AccountModel, AccountViewModel>("Number", number);

        // POST: CustomerController/Transfer
        public string Transfer(AccountViewModel accountInput, uint fromNumber)
        {
            AccountModel? fromAccount = helperService.FindEntityByProperty<AccountModel>("Number", fromNumber);
            AccountModel? transferAccount = helperService.FindEntityByProperty<AccountModel>("Number", accountInput.Number);


            if (fromAccount is null || transferAccount is null)
                return "AccountNotFound";

            if (fromAccount.Balance < accountInput.Balance)
                return "InsufficientFunds";

            fromAccount.Balance -= accountInput.Balance;
            transferAccount.Balance += accountInput.Balance;
            dbContext.SaveChanges();
            return "Index";
        }

        // GET: CustomerController/Details
        public AccountViewModel? Details(uint number) =>
            helperService.FindEntityByProperty<AccountModel, AccountViewModel>("Number", number);
    }
}
