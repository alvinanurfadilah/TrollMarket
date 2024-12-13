using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles = "Admin")]
public class HistoryController : Controller
{
    private readonly HistoryService _service;

    public HistoryController(HistoryService service)
    {
        _service = service;
    }

    public IActionResult Index(int pageNumber = 1, int buyer = 0, int seller = 0)
    {
        var viewModel = _service.Get(pageNumber, buyer, seller);
        return View(viewModel);
    }
}
