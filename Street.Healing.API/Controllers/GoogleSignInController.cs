using Microsoft.AspNetCore.Mvc;
using Street.Healing.API.Context;
using Street.Healing.API.Helpers;
using Street.Healing.API.RequestsData;

namespace Street.Healing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleSignInController(ILogger<GoogleSignInController> logger) : ControllerBase
    {
        private readonly ILogger<GoogleSignInController> _logger = logger;

        [HttpPost("GoogleAuthenticate")]
        public async Task<IActionResult> AuthenticateWGoogle()
        {
            try
            {
                

                return  Ok(new
                {
                    Status = 200,
                    Message = ErrorMessages.UserAdded
                
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"Exception during the process of adding the user : {ex.Message} ");
                return StatusCode(500, new { Status = 500, Message = ErrorMessages.OtpFailed });
            }


        }



    }
}
