using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI.Carts;

[Route("api/v1/cart")]
[ApiController]
[Authorize]
public class CartController : ControllerBase
{
    private readonly CartService _service;

    public CartController(CartService service)
    {
        _service = service;
    }

    [HttpGet("detail/{id}")]
    public IActionResult GetDetail(int id)
    {
        var dto = _service.GetDetail(id);
        return Ok(dto);
    }

    [HttpGet]
    public IActionResult Get()
    {
        var dto = _service.Get();
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Insert(CartFormDTO dto)
    {
        _service.Insert(dto);
        return Ok();
    }
}
