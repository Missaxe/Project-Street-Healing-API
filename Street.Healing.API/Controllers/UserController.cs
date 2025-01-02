using Microsoft.AspNetCore.Mvc;
using Street.Healing.API.Context;
using Street.Healing.API.Helpers;
using Street.Healing.API.RequestsData;
using Street.Healing.API.Services;
using System.Collections;

namespace Street.Healing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
         private readonly IEmailServices _emailServices;
        private readonly IPasswordServices _passwordServices;
        private readonly Dictionary<string, string> _usersoken = new Dictionary<string, string>();


        public UserController(IUserServices userServices, IPasswordServices passwordServices, IEmailServices emailServices)
        {
            _userServices = userServices;
            _emailServices = emailServices;
            _passwordServices = passwordServices;
            _emailServices = emailServices;
        }


        /// <summary>
        /// HttpPost to Login User
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)

        {
            if (userObj == null)
                return BadRequest();

            var user = await _userServices.GetUserAsync(userObj.Email);

            if (user == null)
                return NotFound(new { Message = "User not found!" });

            if (!_passwordServices.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect" });
            }


            return Ok(new
            {
                Status = 200,
                Message = "User Connected!"
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
            if (userObj == null)
                return BadRequest(new { Message = "Data is Empty , No Data to be registred" });

            // check email
            if (await _userServices.CheckEmailExistAsync(userObj.Email))
                return BadRequest(new { Message = "This email already exist, try with another one" });


            if (!string.Equals(userObj.Password, userObj.ConfirmPassword))
                return BadRequest(new { Message = "Passwords are not the same"});

            var passMessage = _passwordServices.CheckPasswordStrength(userObj.Password);
            if(!string.IsNullOrEmpty(passMessage))
                return BadRequest(new { Message = passMessage.ToString() });

            userObj.Password = _passwordServices.HashPassword(userObj.Password);
            userObj.IsEmailValid = _emailServices.IsValidEmail(userObj.Email);
            userObj.DateCreated = DateTime.Now;

            //Map from UserRequest to User
            var mapper = MapperConfig.InitializeAutomapper();
            var userObject = mapper.Map<User>(userObj);

            //Add user into database 
            await _userServices.AddUserAsync(userObject);

            return Ok(new
            {
                Status = 200,
                Message = "User Added!",
                Id = userObject.Id
            });
        }

        [HttpPost("sendToken")]
        public async Task<IActionResult> SendToken([FromBody] int id)
        {
  
            string email = _userServices.GetUserEmailbyIdAsync(id);
            string token = _emailServices.CreateJwt();
            var message = new Message(new string[] { email! }, "OTP Confrimation", token);
            await _emailServices.SendEmailAsync(message);
            return Ok(new
            {
                Status = 200,
                Message = "Email sent!"
            });
        }
    }
}
