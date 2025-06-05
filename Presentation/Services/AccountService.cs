using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Presentation.Data;

namespace Presentation.Services;

public class AccountService(UserManager<IdentityUser> userManager) : IAccountSerivce
{
    private readonly UserManager<IdentityUser> _userManager = userManager;

    private const string VerificationUrl = "https://dennis-verificationservice-d0fjb4c5bsfgfcat.swedencentral-01.azurewebsites.net/api/verification/send";

    public async Task<RegisterResult> RegisterAsync(RegisterModel model)
    {

        var user = new IdentityUser
        {
            UserName = model.UserName,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new RegisterResult<string>
            {
                Success = false,
                Error = errors
            };
        }

        

        try
        {
            var payload = new { user.Email };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var response = await client.PostAsync(VerificationUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                return new RegisterResult<string>
                {
                    Success = false,
                    Error = "User created, but verification code could not be sent."
                };
            }
        }
        catch (Exception ex)
        {
            return new RegisterResult<string>
            {
                Success = false,
                Error = $"User created, but error when contacting verification service: {ex.Message}"
            };
        }

        return new RegisterResult<string> { Success = true, Result = user.Id };
    }


}
