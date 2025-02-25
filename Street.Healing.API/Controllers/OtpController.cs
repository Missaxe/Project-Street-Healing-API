using Microsoft.AspNetCore.Mvc;
using Street.Healing.DAO.Repository;
using Street.Healing.Business.Core.Core.Services;
using Street.Healing.Business.Tech.Helpers;
using Street.Healing.Business.Tech.MailStyling;
using Street.Healing.Business.Tech.Tech.Services;

namespace Street.Healing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController(IUserServices userServices, IPasswordServices passwordServices, IEmailServices emailServices, ILogger<OtpController> logger) : ControllerBase
    {
        private readonly IUserServices _userServices = userServices;
        private readonly IEmailServices _emailServices = emailServices;
        private readonly IPasswordServices _passwordServices = passwordServices;
        private readonly ILogger<OtpController> _logger = logger;

        /// <summary>
        /// Send Otp to confirm email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("sendOtp")]
        public async Task<IActionResult> SendToken([FromBody] int id)
        {

            try
            {

                string email = await userServices.GetUserEmailbyIdAsync(id);
                string otp = _passwordServices.CreateJwt();
                var htmlMail = new HtmlMail(otp);
                var message = new Message(
                    [email!],
                    ConstMessages.OtpConfirmation,
                    htmlMail.HtmlBody,
                    true);
                await _emailServices.SendEmailAsync(message);
                return Ok(new
                {
                    Status = 200,
                    id,
                    otp,
                    Message = ConstMessages.EmailSent
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ConstMessages.ExceptionSendToken} {ex.Message}");
                return StatusCode(500, new { Status = 500, Message = ConstMessages.UserNotAdded });
            }
        }
    }
}
