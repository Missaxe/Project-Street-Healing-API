using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Street.Healing.API.Context;
using Street.Healing.API.Helpers;
using Street.Healing.API.MailStyling;
using Street.Healing.API.RequestsData;
using Street.Healing.API.Services;
using System.Collections;
using static Street.Healing.API.Services.PasswordServices;

namespace Street.Healing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
  
        private readonly IUserServices _userServices;
        private readonly IEmailServices _emailServices;
        private readonly IPasswordServices _passwordServices;
        private readonly ILogger<UserController> _logger ;




        public UserController(IUserServices userServices, IPasswordServices passwordServices, IEmailServices emailServices,ILogger<UserController> logger)
        {
            _userServices = userServices;
            _emailServices = emailServices;
            _passwordServices = passwordServices;
            _emailServices = emailServices;
            _logger = logger;
        }


        /// <summary>
        /// HttpPost to Login User
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserLogin userObj)

        {
            if (userObj == null)
                return BadRequest();

            var user = await _userServices.GetUserAsync(userObj.emailValue);

            if (user == null)
                return NotFound(new { Message = ErrorMessages.UserNotFound});

            if (!_passwordServices.VerifyPassword(userObj.passwordValue, user.HashPassword,user.SaltPassword))
            {
                return BadRequest(new { Message = ErrorMessages.IncorrectPasword });
            }


            return Ok(new
            {
                Status = 200,
                Message = ErrorMessages.UserLogged
            });
        }

        /// <summary>
        /// HttpPost to Add a new user in database
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserClient userObj)
        {
            try
            {
                if (userObj == null)
                    return BadRequest(new { Message = ErrorMessages.EmptyUserObj });

                // check email
                if (await _userServices.CheckEmailExistAsync(userObj.Email))
                    return BadRequest(new { Message = ErrorMessages.EmailAlreadyExists });

                if (string.IsNullOrEmpty(userObj.Password))
                    return BadRequest(new { Message = ErrorMessages.EmptyPasword });


                if (!string.Equals(userObj.Password, userObj.ConfirmPassword))
                    return BadRequest(new { Message = ErrorMessages.PasswordsNotTheSame });

                var passMessage = DataValidators.IsPasswordValid(userObj.Password);

                userObj.IsEmailValid = DataValidators.IsValidEmail(userObj.Email);
                userObj.DateCreated = DateTime.Now;

                //Map from UserRequest to User
                var mapper = MapperConfig.InitializeAutomapper();
                var userObject = mapper.Map<User>(userObj);

                //Add user into database 
                await _userServices.AddUserAsync(userObject);

                return Ok(new
                {
                    Status = 200,
                    Message = ErrorMessages.UserAdded,
                    userObject.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"Exception during the process of adding the user : {ex.Message} ");
                return StatusCode(500, new { Status = 500, Message = ErrorMessages.OtpFailed });
            }


        }

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

                string email = await _userServices.GetUserEmailbyIdAsync(id);
                string otp = _passwordServices.CreateJwt();
                var htmlMail = new HtmlMail(otp);
                var message = new Message(
                    new string[] { email! },
                    "OTP Confirmation",
                    htmlMail.HtmlBody,
                    true);
                await _emailServices.SendEmailAsync(message);
                return Ok(new
                {
                    Status = 200,
                    id,
                    otp,
                    Message = ErrorMessages.EmailSent
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SendToken: {ex.Message}");
                return StatusCode(500, new { Status = 500, Message = ErrorMessages.UserNotAdded });
            }
        }
    }
}
