using Microsoft.AspNetCore.Mvc;
using Street.Healing.API.Context;
using Street.Healing.API.Helpers;
using Street.Healing.API.RequestsDto.User;
using Street.Healing.API.Services;

namespace Street.Healing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IUserServices userServices, IPasswordServices passwordServices, ILogger<LoginController> logger) : ControllerBase
    {
        private readonly IUserServices _userServices = userServices;
        private readonly IPasswordServices _passwordServices = passwordServices;
        private readonly ILogger<LoginController> _logger = logger;

        /// <summary>
        /// HttpPost to Login User
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserClientDto userObj)
        {
            try
            {
                if (userObj == null)
                    return BadRequest();

                var user = await _userServices.GetUserAsync(userObj.Email);

                if (user == null)
                    return NotFound(new { Message = ResponseMessages.UserNotFound });

                if (!_passwordServices.VerifyPassword(userObj.Password, user.HashPassword, user.SaltPassword))
                {
                    return BadRequest(new { Message = ResponseMessages.IncorrectPasword });
                }

                return Ok(new
                {
                    Status = 200,
                    Message = ResponseMessages.UserLogged
                });

            }
            catch(Exception ex)
            {
                _logger.LogError(message: $"Exception during the process of adding the user : {ex.Message} ");
                return StatusCode(500, new { Status = 500, Message = ResponseMessages.OtpFailed });

            }

        }

    }
}
