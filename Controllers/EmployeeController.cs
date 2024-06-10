using eBanking.Services;
using eBanking.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eBanking.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController(EmployeeService employeeService) : Controller
    {
        // GET: EmployeeController
        [HttpGet]
        public ActionResult Index() =>
            View(employeeService.Index());

        // GET: EmployeeController/Create
        [HttpGet]
        public ActionResult Create() =>
            View();

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BankUserViewModel userInput) =>
            RedirectToAction(await employeeService.Create(userInput, ModelState));

        // GET: EmployeeController/AddAccount
        [HttpGet]
        public ActionResult AddAccount() =>
            View();

        // POST: EmployeeController/AddAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAccount(AccountViewModel accountInput) =>
            RedirectToAction(employeeService.AddAccount(accountInput));

        // GET: EmployeeController/DeleteCustomer
        [HttpGet]
        public ActionResult DeleteCustomer() =>
            View();

        // POST: EmployeeController/DeleteCustomer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomer(BankUserViewModel userInput) =>
            RedirectToAction(employeeService.DeleteCustomer(userInput));

        // GET: EmployeeController/DeleteAccount
        [HttpGet]
        public ActionResult DeleteAccount() =>
            View();

        // POST: EmployeeController/DeleteAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccount(AccountViewModel accountInput) =>
            RedirectToAction(employeeService.DeleteAccount(accountInput));

        // GET: EmployeeController/Edit/afm
        [HttpGet]
        [Route("/Employee/Edit/{afm}")]
        public ActionResult Edit(int afm) =>
            employeeService.Edit(afm) is BankUserViewModel account ? View(account) : RedirectToAction("CustomerNotFound");

        // POST: EmployeeController/Edit/afm
        [HttpPost]
        [Route("/Employee/Edit/{currentAFM}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankUserViewModel userInput, int currentAFM) =>
            RedirectToAction(employeeService.Edit(userInput, currentAFM));

        // GET: EmployeeController/FindCustomer
        [HttpGet]
        public ActionResult FindCustomer() =>
            View();

        // POST: EmployeeController/FindCustomer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FindCustomer(BankUserViewModel userInput) =>
            employeeService.FindCustomer(userInput) is int customerAFM
                ? RedirectToAction("FindCustomerDetails", new { userAFM = customerAFM })
                : RedirectToAction("CustomerNotFound");

        // GET: EmployeeController/FindCustomerDetails
        [HttpGet]
        public ActionResult FindCustomerDetails(int userAFM) =>
            View(employeeService.FindCustomerDetails(userAFM));

        // GET: EmployeeController/EditAccount
        [HttpGet]
        public ActionResult EditAccount() =>
            View();

        // POST: EmployeeController/EditAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount(AccountViewModel accountInput) =>
            employeeService.EditAccount(accountInput) is uint accountNumber
                ? RedirectToAction("EditAccountDetails", new { number = accountNumber })
                : RedirectToAction("AccountNotFound");

        // GET: EmployeeController/EditAccountDetails/number
        [HttpGet]
        [Route("/Employee/EditAccountDetails/{number}")]
        public ActionResult EditAccountDetails(uint number) =>
            View(employeeService.EditAccountDetails(number));

        // POST: EmployeeController/EditAccountDetails/number
        [HttpPost]
        [Route("/Employee/EditAccountDetails/{currentNumber}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountDetails(AccountViewModel accountInput, uint currentNumber) =>
            RedirectToAction(employeeService.EditAccountDetails(accountInput, currentNumber));

        // GET: EmployeeController/CustomerNotFound
        [HttpGet]
        public ActionResult CustomerNotFound() =>
            View();

        // GET: EmployeeController/AccountNotFound
        [HttpGet]
        public ActionResult AccountNotFound() =>
            View();

        // GET: EmployeeController/AccountAlreadyExists
        [HttpGet]
        public ActionResult AccountAlreadyExists() =>
            View();
    }
}
