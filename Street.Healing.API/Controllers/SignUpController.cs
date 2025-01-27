using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Street.Healing.API.Context.User;
using Street.Healing.API.Helpers;
using Street.Healing.API.MailStyling;
using Street.Healing.API.RequestsDto.User;
using Street.Healing.API.Services;
using System.Collections;
using static Street.Healing.API.Services.PasswordServices;

namespace Street.Healing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController(IUserServices userServices, ILogger<SignUpController> logger) : ControllerBase
    {
  
        private readonly IUserServices _userServices = userServices;
        private readonly ILogger<SignUpController> _logger = logger;


        /// <summary>
        /// HttpPost to Add a new user in database
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserClientDto userObj)
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


    }
}
