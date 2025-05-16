using Microsoft.AspNetCore.Mvc;
using Presentation.Services;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(IAccountSerivce accountService) : ControllerBase
{
    private readonly IAccountSerivce _accountService = accountService;
}
