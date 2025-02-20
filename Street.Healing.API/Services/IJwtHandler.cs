using Google.Apis.Auth;
using Street.Healing.API.RequestsDto.GoogleSignIn;
using Street.Healing.DTO.ModelsDTO;

namespace Street.Healing.API.Services
{
    public interface IJwtHandler
    {
        Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(UserGoogleDTO externalAuth);
        //Task<string> GenerateToken(GoogleUser user);
    }
}
