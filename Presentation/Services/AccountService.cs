using Microsoft.AspNetCore.Identity;

namespace Presentation.Services;

public class AccountService(UserManager<IdentityUser> userManager) : IAccountSerivce
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
}
