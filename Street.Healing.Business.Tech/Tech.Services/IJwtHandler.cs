using Google.Apis.Auth;
using Street.Healing.DTO.ModelsDTO;

namespace Street.Healing.Business.Tech.Tech.Services
{
    public interface IJwtHandler
    {
        Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(UserGoogleDTO externalAuth);
        //Task<string> GenerateToken(GoogleUser user);
    }
}
