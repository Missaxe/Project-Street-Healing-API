using Google.Apis.Auth;
using Street.Healing.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.DAO.Repository
{
    public interface IUserGoogleRepository
    {
        Task<UserGoogle> LoginOrCreateUserAsync(string? provider, GoogleJsonWebSignature.Payload payload);
    }
}
