using Microsoft.AspNetCore.Mvc;
using Presentation.Data;
using Presentation.Services;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(IAccountSerivce accountService) : ControllerBase
{
    private readonly IAccountSerivce _accountService = accountService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
                    
       
        try
        {
            var userId = await _accountService.RegisterAsync(model);
            return Ok(new { message = "User created", userId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    
}
