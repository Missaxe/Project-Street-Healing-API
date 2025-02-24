using Microsoft.AspNetCore.Mvc;
using Street.Healing.API.Helpers;
using Street.Healing.DAO.Models;
using Street.Healing.DAO.Repository;
using Street.Healing.DTO.Mapping;
using Street.Healing.DTO.ModelsDTO;

namespace Street.Healing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController(IUserRepository userRepository, ILogger<SignUpController> logger) : ControllerBase
    {
  
        private readonly IUserRepository _userRepository = userRepository;
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
                if (await _userRepository.CheckEmailExistAsync(userObj.Email))
                    return BadRequest(new { Message = ConstMessages.EmailAlreadyExists });

                if (string.IsNullOrEmpty(userObj.Password))
                    return BadRequest(new { Message = ConstMessages.EmptyPasword });


                if (!string.Equals(userObj.Password, userObj.ConfirmPassword))
                    return BadRequest(new { Message = ConstMessages.PasswordsNotTheSame });

                var passMessage = DataValidators.IsPasswordValid(userObj.Password);

                userObj.IsEmailValid = DataValidators.IsValidEmail(userObj.Email);
                userObj.DateCreated = DateTime.Now;

                //Map from UserRequest to User
                var mapper = UserMapperConfig.InitializeAutomapper();
                var userObject = mapper.Map<User>(userObj);

                //Add user into database 
                await _userRepository.AddUserAsync(userObject);

                return Ok(new
                {
                    Status = 200,
                    Message = ConstMessages.UserAdded,
                    userObject.Id
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
