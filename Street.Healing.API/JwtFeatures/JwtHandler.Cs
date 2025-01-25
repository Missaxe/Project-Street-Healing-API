using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Street.Healing.API.Context;
using Street.Healing.API.Controllers;
using Street.Healing.API.RequestsDto.GoogleSignIn;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Street.Healing.API.JwtFeatures
{
    public class JwtHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly IConfigurationSection _goolgeSettings;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<JwtHandler> _logger;
        public JwtHandler(IConfiguration configuration, UserManager<User> userManager, ILogger<JwtHandler> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
            _goolgeSettings = _configuration.GetSection("GoogleAuthSettings");
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _goolgeSettings.GetSection("clientId").Value ?? String.Empty }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
                return payload;
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"Exception during the process of verifying google token : {ex.Message} ");
                return null;
            }
        }


    }
}
