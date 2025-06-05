using Presentation.Data;

namespace Presentation.Services;

public  interface IAccountSerivce
{
    Task<RegisterResult> RegisterAsync(RegisterModel model);
}
