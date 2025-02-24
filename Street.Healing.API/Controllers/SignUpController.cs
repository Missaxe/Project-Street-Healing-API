using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Street.Healing.API.Helpers;
using Street.Healing.Business.Core.Core.Repository;
using Street.Healing.Business.Core.Core.Services;
using Street.Healing.DAO.Models;
using Street.Healing.DAO.Repository;
using Street.Healing.DTO.Mapping;
using Street.Healing.DTO.ModelsDTO;

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
        public async Task<IActionResult> AddUserAsync([FromBody] UserDTO userObj)
        {
            try
            {
                if (userObj == null)
                    return BadRequest(new { Message = ConstMessages.EmptyUserObj });

                // check email
                if (await _userServices.CheckIfEmailExistAsync(userObj.Email))
                    return BadRequest(new { Message = ConstMessages.EmailAlreadyExists });

                if (string.IsNullOrEmpty(userObj.Password))
                    return BadRequest(new { Message = ConstMessages.EmptyPasword });


                if (!string.Equals(userObj.Password, userObj.ConfirmPassword))
                    return BadRequest(new { Message = ConstMessages.PasswordsNotTheSame });

                bool IsPasswordValid = DataValidators.IsPasswordValid(userObj.Password);

                userObj.IsEmailValid = DataValidators.IsValidEmail(userObj.Email);

                if(IsPasswordValid && userObj.IsEmailValid)
                {
                    userObj.DateCreated = DateTime.Now;

                    //Generate Tuple of Hash/Salt Passwords
                    userObj.HashedPass = PasswordHash.GenerateSaltedHash(64, userObj.Password);

                    //Map from UserRequest to User
                    var mapper = UserMapperConfig.InitializeAutomapper();
                    var userObject = mapper.Map<User>(userObj);

                    //Add user into database 
                    await _userServices.AddUserAsync(userObject);

                    return Ok(new
                    {
                        Status = 200,
                        Message = ConstMessages.UserAdded,
                        userObject.Id
                    });
                }

                return Unauthorized(new
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = ConstMessages.Unauthorized
                  
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"{ConstMessages.ExceptionSignUpUser} {ex.Message} ");
                return StatusCode(500, new { Status = 500, Message = ConstMessages.OtpFailed });
            }


        }


    }
}
