using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI.Accounts;

[Route("api/v1/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Login (AccountRequestDTO request)
    {
        try
        {
            var response = _service.GetToken(request);
            return Ok(response);
        }
        catch (System.Exception exception)
        {
            return Unauthorized(exception.Message);
        }
    }

    // [HttpGet]
    // public IActionResult Get(string username)
    // {
    //     var dto = _service.Get(username);
    //     return Ok(dto);
    // }

    [HttpPatch]
    [Authorize]
    public IActionResult Patch(AccountFormDTO dto)
    {
        _service.Update(dto);
        return Ok();
    }
}
