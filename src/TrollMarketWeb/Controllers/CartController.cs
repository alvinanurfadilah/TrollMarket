using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;
using TrollMarketWeb.ViewModels.Cart;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles = "Buyer")]
public class CartController : Controller
{
    private readonly CartService _service;

    public CartController(CartService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        string? username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value??string.Empty;
        var model = _service.Get(username);
        return View(model);
    }

    [HttpGet("cart/delete/{id}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return RedirectToAction("Index");
    }

    public IActionResult Update(CartIndexViewModel viewModel)
    {
        try
        {
            string? username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value??string.Empty;
            _service.Get(username);
            _service.Update(viewModel, username);
            return RedirectToAction("Index", "Profile");
        }
        catch (System.Exception exception)
        {
            ViewBag.Message = exception.Message;
        }

        string? user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value??string.Empty;
        var model = _service.Get(user);
        return View("Index", model);
    }
}
