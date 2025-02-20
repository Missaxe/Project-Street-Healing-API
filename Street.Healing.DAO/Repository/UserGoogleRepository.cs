using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Street.Healing.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.DAO.Repository
{
    internal class UserGoogleRepository : IUserGoogleRepository
    {
        private readonly UserManager<UserGoogle> _userManager;


        public UserGoogleRepository(UserManager<UserGoogle> userManager)
        {

            _userManager = userManager;

        }

        public async Task<UserGoogle> LoginOrCreateUserAsync(string? provider, GoogleJsonWebSignature.Payload payload)
        {

            var info = new UserLoginInfo(provider ?? String.Empty, payload.Subject, provider);

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);

                if (user == null)
                {
                    user = new UserGoogle
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
