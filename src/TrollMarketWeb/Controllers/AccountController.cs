using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;
using TrollMarketWeb.ViewModels;
using TrollMarketWeb.ViewModels.Account;

namespace TrollMarketWeb.Controllers;

public class AccountController : Controller
{
    private readonly AccountSerice _service;

    public AccountController(AccountSerice service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var viewModel = _service.GetLogin();
        return RedirectToAction("Login", viewModel);
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {
        if (User?.Identity?.IsAuthenticated??true)
        {
            return RedirectToAction("Index", "Home");
        }
        var viewModel = _service.GetLogin();
        return View(viewModel);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(AccountLoginViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var ticket = _service.SetLogin(viewModel);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ticket.Principal,
                    ticket.Properties
                );
                return RedirectToAction("Index", "Home");
            }
            catch (System.Exception exception)
            {
                ViewBag.Message = exception.Message;
            }
        }
        var vm = _service.GetLogin();
        return View(vm);
    }

    [HttpGet("RegisterBuyer")]
    public IActionResult RegisterBuyer()
    {
        if (!User?.Identity?.IsAuthenticated??true)
        {
            return View();
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("RegisterBuyer")]
    public IActionResult RegisterBuyer(AccountRegisterViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Register(viewModel);
            var vm = _service.GetLogin();
            return RedirectToAction("Login", "Account", vm);
        }
        return View();
    }

    [HttpGet("RegisterSeller")]
    public IActionResult RegisterSeller()
    {
        if (!User?.Identity?.IsAuthenticated??true)
        {
            return View();
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("RegisterSeller")]
    public IActionResult RegisterSeller(AccountRegisterViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Register(viewModel);
            var vm = _service.GetLogin();
            return RedirectToAction("Login", "Account", vm);
        }
        return View();
    }

    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied()
    {
        return View("AccessDenied");
    }

    [Authorize]
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}
