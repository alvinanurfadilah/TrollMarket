using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles = "Buyer, Seller")]
public class ProfileController : Controller
{
    private readonly ProfileService _service;

    public ProfileController(ProfileService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        string? username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value??string.Empty;
        var model = _service.Get(username);
        ViewData["AccountId"] = model?.Profile.Id;
        return View("Index", model);
    }
}
