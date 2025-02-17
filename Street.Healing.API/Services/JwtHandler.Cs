using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Street.Healing.API.Context.GoogleUser;
using Street.Healing.API.Context.User;
using Street.Healing.API.Controllers;
using Street.Healing.API.Helpers;
using Street.Healing.API.RequestsDto.GoogleSignIn;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Street.Healing.API.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly IConfigurationSection _goolgeSettings;
        private readonly UserManager<GoogleUser> _userManager;
        private readonly ILogger<JwtHandler> _logger;
        public JwtHandler(IConfiguration configuration/*, UserManager<GoogleUser> userManager*/, ILogger<JwtHandler> logger)
        {
            _logger = logger;
            //_userManager = userManager;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
            _goolgeSettings = _configuration.GetSection("GoogleAuthSettings");
        } 

        public async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(ExternalAuthDto externalAuth)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _goolgeSettings.GetSection("clientId").Value ?? string.Empty }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
                return payload;
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"{ConstMessages.ExceptionVerifyToken} {ex.Message} ");
                return null;
            }
        }

        //public async Task<string> GenerateToken(GoogleUser user)
        //{
        //    var signingCredentials = GetSigningCredentials();
        //    var claims = await GetClaims(user);
        //    var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        //    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        //    return token;
        //}

        //private SigningCredentials GetSigningCredentials()
        //{
        //    var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
        //    var secret = new SymmetricSecurityKey(key);

        //    return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        //}

        //private async Task<List<Claim>> GetClaims(GoogleUser user)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.Email)
        //    };

        //    var roles = await _userManager.GetRolesAsync(user);
        //    foreach (var role in roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }

        //    return claims;
        //}

        //private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        //{
        //    var tokenOptions = new JwtSecurityToken(
        //        issuer: _jwtSettings["validIssuer"],
        //        audience: _jwtSettings["validAudience"],
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
        //        signingCredentials: signingCredentials);

        //    return tokenOptions;
        //}


    }
}
