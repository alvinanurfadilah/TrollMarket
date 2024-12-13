using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles = "Buyer")]
public class ShopController : Controller
{
    private readonly ShopService _service;

    public ShopController(ShopService service)
    {
        _service = service;
    }

    public IActionResult Index(int pageNumber = 1, string name = "", string categoryName = "", string description = "")
    {
        string? username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value??string.Empty;
        var model = _service.GetId(username);
        ViewBag.AccountId = model.Id;
        var viewModel = _service.Get(pageNumber, name, categoryName, description);
        return View(viewModel);
    }
}
