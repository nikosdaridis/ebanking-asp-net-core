using eBanking.Services;
using eBanking.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eBanking.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: CustomerController
        [HttpGet]
        public ActionResult Index()
        {
            return View(_customerService.Index(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        // GET: CustomerController/Deposit
        [HttpGet]
        [Route("/Customer/Deposit/{number}")]
        public ActionResult Deposit(uint number)
        {
            return _customerService.Deposit(number) ? View(new AccountViewModel()) : RedirectToAction("AccountNotFound");
        }

        // POST: CustomerController/Deposit
        [HttpPost]
        [Route("/Customer/Deposit/{number}")]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit(AccountViewModel accountInput)
        {
            return RedirectToAction(_customerService.Deposit(accountInput));
        }

        // GET: CustomerController/Transfer
        [HttpGet]
        [Route("/Customer/Transfer/{number}")]
        public ActionResult Transfer(uint number)
        {
            AccountViewModel? account = _customerService.Transfer(number);
            return account != null ? View(account) : RedirectToAction("AccountNotFound");
        }

        // POST: CustomerController/Transfer
        [HttpPost]
        [Route("/Customer/Transfer/{fromNumber}")]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(AccountViewModel accountInput, uint fromNumber)
        {
            return RedirectToAction(_customerService.Transfer(accountInput, fromNumber));
        }

        // GET: CustomerController/Details
        [HttpGet]
        [Route("/Customer/Details/{number}")]
        public ActionResult Details(uint number)
        {
            AccountViewModel? account = _customerService.Details(number);
            return account != null ? View(account) : RedirectToAction("AccountNotFound");
        }

        // GET: CustomerController/AccountNotFound
        [HttpGet]
        public ActionResult AccountNotFound()
        {
            return View();
        }

        // GET: CustomerController/InsufficientFunds
        [HttpGet]
        public ActionResult InsufficientFunds()
        {
            return View();
        }
    }
}
