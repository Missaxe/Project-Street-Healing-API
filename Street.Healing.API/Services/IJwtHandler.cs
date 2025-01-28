using Google.Apis.Auth;
using Street.Healing.API.Context.GoogleUser;
using Street.Healing.API.RequestsDto.GoogleSignIn;

namespace Street.Healing.API.Services
{
    public interface IJwtHandler
    {
        Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(ExternalAuthDto externalAuth);
        //Task<string> GenerateToken(GoogleUser user);
    }
}
