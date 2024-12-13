using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;
using TrollMarketWeb.ViewModels.Admin;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly AdminService _service;

    public AdminController(AdminService service)
    {
        _service = service;
    }

    [HttpGet("admin/Form")]
    public IActionResult Add()
    {
        return View("Form");
    }

    [HttpPost("admin/Form")]
    public IActionResult Insert(AdminFormViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Insert(viewModel);
            return RedirectToAction("Form");
        }
        return View("Form");
    }
}
