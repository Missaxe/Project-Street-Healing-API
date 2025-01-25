using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Street.Healing.API.Context;
using Street.Healing.API.Helpers;
using Street.Healing.API.JwtFeatures;
using Street.Healing.API.RequestsDto.GoogleSignIn;

namespace Street.Healing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleSignInController(ILogger<GoogleSignInController> logger, UserManager<User> userManager, JwtHandler jwtHandler) : ControllerBase
    {
        private readonly ILogger<GoogleSignInController> _logger = logger;
        private readonly JwtHandler _jwtHandler = jwtHandler;
        private readonly UserManager<User> _userManager = userManager;

        [HttpPost("register")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthDto externalAuth)
        {
            try
            {
                var payload = await _jwtHandler.VerifyGoogleToken(externalAuth);
                if (payload == null)
                    return BadRequest("Invalid External Authentication.");

                var info = new UserLoginInfo(externalAuth.Provider ?? String.Empty, payload.Subject, externalAuth.Provider);

                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(payload.Email);

                    if (user == null)
                    {
                        user = new User { Email = payload.Email, FirstName = payload.Name, LastName = payload.FamilyName };
                        await _userManager.CreateAsync(user);

                        //prepare and send an email for the email confirmation

                        await _userManager.AddToRoleAsync(user, "Viewer");
                        await _userManager.AddLoginAsync(user, info);
                    }
                    else
                    {
                        await _userManager.AddLoginAsync(user, info);
                    }
                }

                if (user == null)
                    return BadRequest("Invalid External Authentication.");

                //check for the Locked out account

                return Ok(new
                {
                    Status = 200,
                    Message = ErrorMessages.UserAdded,

                });
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"Exception during the process of adding the user with Google Account : {ex.Message} ");
                return StatusCode(500, new { Status = 500, Message = ErrorMessages.GoogleSignInFailed });
            }
        }



    }
}
