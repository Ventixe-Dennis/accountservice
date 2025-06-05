using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Presentation.Data;

namespace Presentation.Services;

public class AccountService(UserManager<IdentityUser> userManager, IConfiguration config) : IAccountSerivce
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly IConfiguration _config = config;

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

        var verificationUrl = _config["VerificationService:BaseUrl"];
        if (string.IsNullOrWhiteSpace(verificationUrl))
        {
            return new RegisterResult<string>
            {
                Success = false,
                Error = "Verification service URL is not configured"
            };
        }

        try
        {
            var payload = new { user.Email };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var response = await client.PostAsync(verificationUrl, content);

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
