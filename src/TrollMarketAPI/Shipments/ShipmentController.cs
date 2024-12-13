using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI.Shipments;

[Route("api/v1/shipment")]
[ApiController]
[Authorize]
public class ShipmentController : ControllerBase
{
    private readonly ShipmentService _service;

    public ShipmentController(ShipmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get(int id)
    {
        var dto = _service.Get(id);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Insert(ShipmentFormDTO dto)
    {
        _service.Insert(dto);
        return Ok();
    }

    [HttpPut]
    public IActionResult Put(ShipmentFormDTO dto)
    {
        _service.Update(dto);
        return Ok();
    }
}
