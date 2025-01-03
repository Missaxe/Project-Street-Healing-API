using Microsoft.AspNetCore.Mvc;
using Street.Healing.API.Context;
using Street.Healing.API.Helpers;
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
        private readonly ICache<string,string> _cache;




        public UserController(IUserServices userServices, IPasswordServices passwordServices, IEmailServices emailServices, ICache<string, string> cache)
        {
            _userServices = userServices;
            _emailServices = emailServices;
            _passwordServices = passwordServices;
            _emailServices = emailServices;
            _cache = cache;
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
                return NotFound(new { Message = "User not found!" });

            if (!_passwordServices.VerifyPassword(userObj.passwordValue, user.HashPassword,user.SaltPassword))
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

            HashSalt hashSalt = GenerateSaltedHash(64, userObj.Password);

            //userObj.Password = _passwordServices.HashPassword(userObj.Password);
            userObj.HashPassword = hashSalt.Hash;
            userObj.SaltPassword = hashSalt.Salt;
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
                userObject.Id
            });
        }

        /// <summary>
        /// Send Otp to confirm email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("sendOtp")]
        public async Task<IActionResult> SendToken([FromBody] int id)
        {

            string email = _userServices.GetUserEmailbyIdAsync(id);
            string otp = _emailServices.CreateJwt();
            var message = new Message(new string[] { email! }, "OTP Confrimation", otp);
            await _emailServices.SendEmailAsync(message);
            _cache.Store(email, otp, TimeSpan.FromMinutes(2));
            return Ok(new
            {
                Status = 200,
                id,
                otp,
                Message = "Email sent!"
            });
        }
    }
}
