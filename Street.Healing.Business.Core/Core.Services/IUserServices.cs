using Street.Healing.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.Business.Core.Core.Services
{
    public interface IUserServices
    {
        public Task AddUserAsync(User userObj);

        public Task<User> GetUserAsync(string userEmail);

        public Task<bool> CheckIfEmailExistAsync(string email);

        public Task<bool> CheckIfUsernameExistAsync(string firstName, string lastName);

        public Task<string> GetUserEmailbyIdAsync(int id);
    }
}
