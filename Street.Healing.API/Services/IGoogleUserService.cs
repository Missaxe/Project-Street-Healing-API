using Google.Apis.Auth;
using Street.Healing.API.Context.GoogleUser;

namespace Street.Healing.API.Services
{
    public interface IGoogleUserService
    {
        Task<GoogleUser> LoginOrCreateUserAsync(string? provider, GoogleJsonWebSignature.Payload payload);
    }
}
