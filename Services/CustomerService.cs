using AutoMapper;
using eBanking.Areas.Identity.Data;
using eBanking.Models;
using eBanking.ViewModels;

namespace eBanking.Services
{
    public class CustomerService
    {
        private readonly BankDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(BankDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: CustomerController
        public IEnumerable<AccountViewModel> Index(string? userId)
        {
            if (userId == null) return [];

            BankUserViewModel? user = FindBankUserViewModelById(userId);
            if (user == null) return [];

            return _mapper.Map<IEnumerable<AccountViewModel>>(_context.Accounts.Where(account => account.AFM == user.AFM));
        }

        // GET: CustomerController/Deposit
        public bool Deposit(uint number)
        {
            return AccountExists(number);
        }

        // POST: CustomerController/Deposit
        public string Deposit(AccountViewModel accountInput)
        {
            AccountModel? account = FindAccountModelByNumber(accountInput.Number);

            if (account == null) return "AccountNotFound";

            account.Balance += accountInput.Balance;
            _context.SaveChanges();
            return "Index";
        }

        // GET: CustomerController/Transfer
        public AccountViewModel? Transfer(uint number)
        {
            return FindAccountViewModelByNumber(number);
        }

        // POST: CustomerController/Transfer
        public string Transfer(AccountViewModel accountInput, uint fromNumber)
        {
            AccountModel? fromAccount = FindAccountModelByNumber(fromNumber);
            AccountModel? transferAccount = FindAccountModelByNumber(accountInput.Number);

            if (fromAccount == null || transferAccount == null) return "AccountNotFound";

            if (fromAccount.Balance < accountInput.Balance) return "InsufficientFunds";

            fromAccount.Balance -= accountInput.Balance;
            transferAccount.Balance += accountInput.Balance;
            _context.SaveChanges();
            return "Index";
        }

        // GET: CustomerController/Details
        public AccountViewModel? Details(uint number)
        {
            return FindAccountViewModelByNumber(number);
        }

        /// <summary>
        /// Finds an AccountModel by the specified account number
        /// </summary>
        /// <param name="searchNumber"></param>
        /// <returns>
        /// The AccountModel associated with the specified account number or null if no matching account is found
        /// </returns>
        private AccountModel? FindAccountModelByNumber(uint searchNumber)
        {
            return _context.Accounts.Where(account => account.Number == searchNumber).FirstOrDefault();
        }

        /// <summary>
        /// Finds an AccountViewModel by the specified account number
        /// </summary>
        /// <param name="searchNumber"></param>
        /// <returns>
        /// The AccountViewModel associated with the specified account number or null if no matching account is found
        /// </returns>
        private AccountViewModel? FindAccountViewModelByNumber(uint searchNumber)
        {
            return _mapper.Map<AccountViewModel>(_context.Accounts.Where(account => account.Number == searchNumber).FirstOrDefault());
        }

        /// <summary>
        /// Finds a BankUserViewModel by the specified user Id
        /// </summary>
        /// <param name="searchId"></param>
        /// <returns>
        /// The BankUserViewModel associated with the specified user Id or null if no matching user is found
        /// </returns>
        private BankUserViewModel? FindBankUserViewModelById(string searchId)
        {
            return _mapper.Map<BankUserViewModel>(_context.Users.Where(user => user.Id == searchId).FirstOrDefault());
        }

        /// <summary>
        /// Checks whether an account with the specified account number exists in the database
        /// </summary>
        /// <param name="searchNumber"></param>
        /// <returns>
        /// Returns true if an account with the specified number exists, otherwise returns false
        /// </returns>
        private bool AccountExists(uint searchNumber)
        {
            return _context.Accounts.Any(account => account.Number == searchNumber);
        }
    }
}