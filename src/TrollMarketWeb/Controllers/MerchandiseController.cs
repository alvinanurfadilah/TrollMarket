using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TrollMarketWeb.Services;
using TrollMarketWeb.ViewModels.Merchandise;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles = "Seller")]
public class MerchandiseController : Controller
{
    private readonly MerchandiseService _service;

    public MerchandiseController(MerchandiseService service)
    {
        _service = service;
    }

    public IActionResult Index(int pageNumber = 1)
    {
        string? username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value??string.Empty;
        var model = _service.Get(username);
        var viewModel = _service.Get(pageNumber, model);
        return View(viewModel);
    }

    [HttpGet("merchandise/form")]
    public IActionResult Add()
    {
        return View("Form");
    }

    [HttpPost("merchandise/form")]
    public IActionResult Insert(MerchandiseFormViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            string? username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value??string.Empty;
            var vm = _service.Get(username);
            viewModel.AccountId = vm;
            _service.Insert(viewModel);
            return RedirectToAction("Index");
        }
        return View("Form");
    }

    [HttpGet("merchandise/form/{id}")]
    public IActionResult Edit(int id)
    {
        var viewModel = _service.GetById(id);
        return View("Form", viewModel);
    }

    [HttpPost("merchandise/form/{id}")]
    public IActionResult Update(MerchandiseFormViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            string? username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value??string.Empty;
            var vm = _service.Get(username);
            viewModel.AccountId = vm;
            _service.Update(viewModel);
            return RedirectToAction("Index");
        }
        return View("Form");
    }

    [HttpGet("discontinue/{id}")]
    public IActionResult Discontinue(int id)
    {
        _service.Discontinue(id);
        return RedirectToAction("Index");
    }

    [HttpGet("merchandise/delete/{id}")]
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
