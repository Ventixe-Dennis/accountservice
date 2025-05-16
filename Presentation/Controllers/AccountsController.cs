using Microsoft.AspNetCore.Mvc;
using Presentation.Services;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(IAccountSerivce accountService) : ControllerBase
{
    private readonly IAccountSerivce _accountService = accountService;

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var result = _accountService.CreateAsync();
    }
}
