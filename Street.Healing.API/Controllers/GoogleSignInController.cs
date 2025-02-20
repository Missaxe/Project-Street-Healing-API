using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Street.Healing.API.Helpers;
using Street.Healing.API.RequestsDto.GoogleSignIn;
using Street.Healing.API.Services;
using Street.Healing.DAO.Repository;
using Street.Healing.DTO.ModelsDTO;
using Twilio.Jwt.AccessToken;

namespace Street.Healing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleSignInController(ILogger<GoogleSignInController> logger,IUserGoogleRepository googleUser, IJwtHandler jwtHandler) : ControllerBase
    {
        private readonly ILogger<GoogleSignInController> _logger = logger;
        private readonly IJwtHandler _jwtHandler = jwtHandler;
        private readonly IUserGoogleRepository _googleUser = googleUser;

        /// <summary>
        /// External Login using Google
        /// </summary>
        /// <param name="externalAuth"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> ExternalLogin([FromBody] UserGoogleDTO externalAuth)
        {
            try
            {
                var payload = await _jwtHandler.VerifyGoogleToken(externalAuth);

                if (payload == null)
                    return BadRequest(ConstMessages.InvalidExterAuthentication);

                var user = await _googleUser.LoginOrCreateUserAsync(externalAuth.Provider, payload);
               
                if (user == null)
                    return BadRequest(ConstMessages.InvalidAuthentication);
             

                return Ok(new AuthResponseDto { IsAuthSuccessful = true, ErrorMessage = ConstMessages.UserAdded, });

            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"{ConstMessages.ExceptionExternalLogin} {ex.Message} ");
                return StatusCode(500, new { Status = 500, Message = ConstMessages.GoogleSignInFailed });
            }
        }



    }
}
