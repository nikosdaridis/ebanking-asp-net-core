using AutoMapper;
using eBanking.Areas.Identity.Data;
using eBanking.Models;
using eBanking.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eBanking.Services
{
    public class EmployeeService
    {
        private readonly BankDbContext _context;
        private readonly UserManager<BankUser> _userManager;
        private readonly IMapper _mapper;

        public EmployeeService(BankDbContext context, UserManager<BankUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: EmployeeController
        public IEnumerable<BankUserViewModel> Index()
        {
            return _mapper.Map<IEnumerable<BankUserViewModel>>(_context.Users.Where(user => user.RoleId == "1"));
        }

        // POST: EmployeeController/Create
        public async Task Create(BankUserViewModel userInput, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return;

            BankUser user = _mapper.Map<BankUser>(userInput);
            user.Email = user.UserName;
            user.RoleId = "1";

            IdentityResult? result = await _userManager.CreateAsync(user, userInput.Password);

            if (result.Succeeded) await _userManager.AddToRoleAsync(user, "Customer");
        }

        // POST: EmployeeController/AddAccount
        public string AddAccount(AccountViewModel accountInput)
        {
            if (FindBankUserByAFM(accountInput.AFM) == null)
                return "CustomerNotFound";

            if (FindAccountModelByNumber(accountInput.Number) != null)
                return "AccountAlreadyExists";

            AccountModel account = _mapper.Map<AccountModel>(accountInput);

            _context.Accounts.Add(account);
            _context.SaveChanges();
            return "Index";
        }

        // POST: EmployeeController/DeleteCustomer
        public string DeleteCustomer(BankUserViewModel userInput)
        {
            BankUser? user = FindBankUserByAFM(userInput.AFM);

            if (user == null) return "CustomerNotFound";

            _context.Users.Remove(user);
            _context.SaveChanges();
            return "Index";
        }

        // POST: EmployeeController/DeleteAccount
        public string DeleteAccount(AccountViewModel accountInput)
        {
            List<AccountModel>? accounts = _context.Accounts.Where(account => account.Number == accountInput.Number).ToList();

            if (accounts.Count == 0) return "AccountNotFound";

            foreach (AccountModel account in accounts)
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
            }
            return "Index";
        }

        // GET: EmployeeController/Edit/afm
        public BankUserViewModel? Edit(int afm)
        {
            BankUserViewModel? user = FindBankUserViewModelByAFM(afm);

            return user;
        }

        // POST: EmployeeController/Edit/afm
        public string Edit(BankUserViewModel userInput, int currentAFM)
        {
            BankUser user = FindBankUserByAFM(currentAFM)!;

            user.FirstName = userInput.FirstName;
            user.LastName = userInput.LastName;
            user.Address = userInput.Address;
            user.Phone = userInput.Phone;
            user.UserName = userInput.UserName;
            user.Email = user.UserName;
            user.NormalizedUserName = user.UserName.ToUpper();
            user.NormalizedEmail = user.Email.ToUpper();

            _context.SaveChanges();
            return "Index";
        }

        // POST: EmployeeController/FindCustomer
        public int FindCustomer(BankUserViewModel userInput)
        {
            BankUser? user = FindBankUserByAFM(userInput.AFM);

            return user != null ? user.AFM : -1;
        }

        // GET: EmployeeController/FindCustomerDetails
        public BankUserViewModel FindCustomerDetails(int userAFM)
        {
            return FindBankUserViewModelByAFM(userAFM)!;
        }

        // POST: EmployeeController/EditAccount
        public int EditAccount(AccountViewModel accountInput)
        {
            AccountModel? account = FindAccountModelByNumber(accountInput.Number);

            return account != null ? account.Number : -1;
        }

        // GET: EmployeeController/EditAccountDetails/number
        public AccountViewModel EditAccountDetails(int accountNumber)
        {
            return FindAccountViewModelByNumber(accountNumber)!;
        }

        // POST: EmployeeController/EditAccountDetails/number
        public string EditAccountDetails(AccountViewModel accountInput, int currentNumber)
        {
            AccountModel? account = FindAccountModelByNumber(currentNumber);

            if (account == null) return "AccountNotFound";

            if (accountInput.Number != currentNumber && FindAccountModelByNumber(accountInput.Number) != null)
                return "AccountAlreadyExists";

            account!.Number = accountInput.Number;
            account.Branch = accountInput.Branch;
            account.Type = accountInput.Type;

            _context.SaveChanges();
            return "Index";
        }

        /// <summary>
        /// Finds a BankUser by the specified user AFM
        /// </summary>
        /// <param name="searchAFM"></param>
        /// <returns>
        /// The BankUser associated with the specified user AFM or null if no matching user is found
        /// </returns>
        private BankUser? FindBankUserByAFM(int searchAFM)
        {
            return _context.Users.Where(user => user.AFM == searchAFM).FirstOrDefault();
        }

        /// <summary>
        /// Finds a BankUserViewModel by the specified user AFM
        /// </summary>
        /// <param name="searchAFM"></param>
        /// <returns>
        /// The BankUserViewModel associated with the specified user AFM or null if no matching user is found
        /// </returns>
        private BankUserViewModel? FindBankUserViewModelByAFM(int searchAFM)
        {
            return _mapper.Map<BankUserViewModel>(_context.Users.Where(user => user.AFM == searchAFM).FirstOrDefault());
        }

        /// <summary>
        /// Finds an AccountModel by the specified account Number
        /// </summary>
        /// <param name="searchNumber"></param>
        /// <returns>
        /// The AccountModel associated with the specified account Number or null if no matching account is found
        /// </returns>
        private AccountModel? FindAccountModelByNumber(int searchNumber)
        {
            return _context.Accounts.Where(account => account.Number == searchNumber).FirstOrDefault();
        }

        /// <summary>
        /// Finds an AccountViewModel by the specified account Number
        /// </summary>
        /// <param name="searchNumber"></param>
        /// <returns>
        /// The AccountViewModel associated with the specified account Number or null if no matching account is found
        /// </returns>
        private AccountViewModel? FindAccountViewModelByNumber(int searchNumber)
        {
            return _mapper.Map<AccountViewModel>(_context.Accounts.Where(account => account.Number == searchNumber).FirstOrDefault());
        }
    }
}
