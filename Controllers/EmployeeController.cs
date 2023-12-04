using eBanking.Services;
using eBanking.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eBanking.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: EmployeeController
        [HttpGet]
        public ActionResult Index()
        {
            return View(_employeeService.Index());
        }

        // GET: EmployeeController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BankUserViewModel userInput)
        {
            return RedirectToAction(await _employeeService.Create(userInput, ModelState) ? "Index" : "SomethingWentWrong");
        }

        // GET: EmployeeController/AddAccount
        [HttpGet]
        public ActionResult AddAccount()
        {
            return View();
        }

        // POST: EmployeeController/AddAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAccount(AccountViewModel accountInput)
        {
            return RedirectToAction(_employeeService.AddAccount(accountInput));
        }

        // GET: EmployeeController/DeleteCustomer
        [HttpGet]
        public ActionResult DeleteCustomer()
        {
            return View();
        }

        // POST: EmployeeController/DeleteCustomer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomer(BankUserViewModel userInput)
        {
            return RedirectToAction(_employeeService.DeleteCustomer(userInput));
        }

        // GET: EmployeeController/DeleteAccount
        [HttpGet]
        public ActionResult DeleteAccount()
        {
            return View();
        }

        // POST: EmployeeController/DeleteAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccount(AccountViewModel accountInput)
        {
            return RedirectToAction(_employeeService.DeleteAccount(accountInput));
        }

        // GET: EmployeeController/Edit/afm
        [HttpGet]
        [Route("/Employee/Edit/{afm}")]
        public ActionResult Edit(int afm)
        {
            BankUserViewModel? account = _employeeService.Edit(afm);
            return account != null ? View(account) : RedirectToAction("CustomerNotFound");
        }

        // POST: EmployeeController/Edit/afm
        [HttpPost]
        [Route("/Employee/Edit/{currentAFM}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankUserViewModel userInput, int currentAFM)
        {
            return RedirectToAction(_employeeService.Edit(userInput, currentAFM));
        }

        // GET: EmployeeController/FindCustomer
        [HttpGet]
        public ActionResult FindCustomer()
        {
            return View();
        }

        // POST: EmployeeController/FindCustomer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FindCustomer(BankUserViewModel userInput)
        {
            int customerAFM = _employeeService.FindCustomer(userInput);

            return customerAFM != -1 ? RedirectToAction("FindCustomerDetails", new { userAFM = customerAFM }) : RedirectToAction("CustomerNotFound");
        }

        // GET: EmployeeController/FindCustomerDetails
        [HttpGet]
        public ActionResult FindCustomerDetails(int userAFM)
        {
            return View(_employeeService.FindCustomerDetails(userAFM));
        }

        // GET: EmployeeController/EditAccount
        [HttpGet]
        public ActionResult EditAccount()
        {
            return View();
        }

        // POST: EmployeeController/EditAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount(AccountViewModel accountInput)
        {
            int accountNumber = _employeeService.EditAccount(accountInput);

            return accountNumber != -1 ? RedirectToAction("EditAccountDetails", new { number = accountNumber }) : RedirectToAction("AccountNotFound");
        }

        // GET: EmployeeController/EditAccountDetails/number
        [HttpGet]
        [Route("/Employee/EditAccountDetails/{number}")]
        public ActionResult EditAccountDetails(int number)
        {
            return View(_employeeService.EditAccountDetails(number));
        }

        // POST: EmployeeController/EditAccountDetails/number
        [HttpPost]
        [Route("/Employee/EditAccountDetails/{currentNumber}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountDetails(AccountViewModel accountInput, int currentNumber)
        {
            return RedirectToAction(_employeeService.EditAccountDetails(accountInput, currentNumber));
        }

        // GET: EmployeeController/CustomerNotFound
        [HttpGet]
        public ActionResult CustomerNotFound()
        {
            return View();
        }

        // GET: EmployeeController/AccountNotFound
        [HttpGet]
        public ActionResult AccountNotFound()
        {
            return View();
        }

        // GET: EmployeeController/AccountAlreadyExists
        [HttpGet]
        public ActionResult AccountAlreadyExists()
        {
            return View();
        }


        // GET: EmployeeController/SomethingWentWrong
        [HttpGet]
        public ActionResult SomethingWentWrong()
        {
            return View();
        }
    }
}
