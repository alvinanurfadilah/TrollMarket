using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles = "Admin")]
public class ShipmentController : Controller
{
    private readonly ShipmentService _service;

    public ShipmentController(ShipmentService service)
    {
        _service = service;
    }

    public IActionResult Index(int pageNumber = 1)
    {
        var viewModel = _service.Get(pageNumber);
        return View(viewModel);
    }

    [HttpGet("stopservice/{id}")]
    public IActionResult StopService(int id)
    {
        _service.StopService(id);
        return RedirectToAction("Index");
    }

    [HttpGet("shipment/delete/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            return View("Delete");
        }
    }
}
