﻿using Microsoft.AspNetCore.Mvc;
using Street.Healing.Business.Core.Core.Repository;
using Street.Healing.Business.Core.Core.Services;
using Street.Healing.Business.Tech.Helpers;
using Street.Healing.Business.Tech.Tech.Services;
using Street.Healing.DAO.Repository;
using Street.Healing.DTO.ModelsDTO;

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
        public async Task<IActionResult> Authenticate([FromBody] UserDTO userObj)
        {
            try
            {
                if (userObj == null)
                    return BadRequest();

                var user = await _userServices.GetUserAsync(userObj.Email);

                if (user == null)
                    return NotFound(new { Message = ConstMessages.UserNotFound });

                if (!DataValidators.VerifyPassword(userObj.Password, user.HashPassword, user.SaltPassword))
                {
                    return BadRequest(new { Message = ConstMessages.IncorrectPasword });
                }

                return Ok(new
                {
                    Status = 200,
                    Message = ConstMessages.UserLogged
                });

            }
            catch(Exception ex)
            {
                _logger.LogError(message: $"{ConstMessages.ExceptionLoginUser} {ex.Message} ");
                return StatusCode(500, new { Status = StatusCodes.Status500InternalServerError, Message = ConstMessages.ExceptionLoginUser });

            }

        }

    }
}
