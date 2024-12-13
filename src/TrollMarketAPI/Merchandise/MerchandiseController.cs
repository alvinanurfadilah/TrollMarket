using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI.Merchandise;

[Route("api/v1/merchandise")]
[ApiController]
[Authorize]
public class MerchandiseController : ControllerBase
{
    private readonly MerchandiseService _service;

    public MerchandiseController(MerchandiseService service)
    {
        _service = service;
    }

    [HttpGet("info/{id}")]
    public IActionResult GetInfo(int id)
    {
        var dto = _service.GetInfo(id);
        return Ok(dto);
    }
}
