using eBanking.Services;
using eBanking.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eBanking.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController(CustomerService customerService) : Controller
    {
        // GET: CustomerController
        [HttpGet]
        public ActionResult Index() =>
            View(customerService.Index(User.FindFirstValue(ClaimTypes.NameIdentifier)));

        // GET: CustomerController/Deposit
        [HttpGet]
        [Route("/Customer/Deposit/{number}")]
        public ActionResult Deposit(uint number) =>
            customerService.Deposit(number) ? View(new AccountViewModel()) : RedirectToAction("AccountNotFound");

        // POST: CustomerController/Deposit
        [HttpPost]
        [Route("/Customer/Deposit/{number}")]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit(AccountViewModel accountInput) =>
            RedirectToAction(customerService.Deposit(accountInput));

        // GET: CustomerController/Transfer
        [HttpGet]
        [Route("/Customer/Transfer/{number}")]
        public ActionResult Transfer(uint number) =>
            customerService.Transfer(number) is AccountViewModel account ? View(account) : RedirectToAction("AccountNotFound");


        // POST: CustomerController/Transfer
        [HttpPost]
        [Route("/Customer/Transfer/{fromNumber}")]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(AccountViewModel accountInput, uint fromNumber) =>
            RedirectToAction(customerService.Transfer(accountInput, fromNumber));

        // GET: CustomerController/Details
        [HttpGet]
        [Route("/Customer/Details/{number}")]
        public ActionResult Details(uint number) =>
            customerService.Details(number) is AccountViewModel account ? View(account) : RedirectToAction("AccountNotFound");

        // GET: CustomerController/AccountNotFound
        [HttpGet]
        public ActionResult AccountNotFound() =>
            View();

        // GET: CustomerController/InsufficientFunds
        [HttpGet]
        public ActionResult InsufficientFunds() =>
            View();
    }
}
