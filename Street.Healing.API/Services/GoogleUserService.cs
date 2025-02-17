using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Street.Healing.API.Context.GoogleUser;
using Street.Healing.API.Context.User;
using Street.Healing.API.RequestsDto.GoogleSignIn;

namespace Street.Healing.API.Services
{
    public class GoogleUserService : IGoogleUserService
    {

        private readonly UserManager<GoogleUser> _userManager;


        public GoogleUserService(UserManager<GoogleUser> userManager)
        {

            _userManager = userManager;
 
        }

        public async Task<GoogleUser> LoginOrCreateUserAsync(string? provider, GoogleJsonWebSignature.Payload payload)
        {

            var info = new UserLoginInfo(provider ?? String.Empty, payload.Subject, provider);

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);

                if (user == null)
                {
                    user = new GoogleUser 
                    { 
                        Email = payload.Email,
                        UserName = payload.Email 
                    };

                    await _userManager.CreateAsync(user);


                    //await _userManager.AddToRoleAsync(user,"ADMIN");
                    await _userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }
            return user;

        }
    }
}
