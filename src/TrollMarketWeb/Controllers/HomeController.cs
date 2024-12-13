using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;

namespace TrollMarketWeb.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ProfileService _service;

    public HomeController(ProfileService service)
    {
        _service = service;
    }
    public IActionResult Index()
    {
        // string? username = User.FindFirst("username")?.Value??string.Empty;
        // var model = _service.Get(username);
        // return View("Index", model);
        return View("Index");
    }
}