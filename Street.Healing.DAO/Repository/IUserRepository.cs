using Street.Healing.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.DAO.Repository
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User userObj);

        public Task<User> GetUserAsync(string userEmail);

        public Task<bool> CheckEmailExistAsync(string email);

        public Task<bool> CheckUsernameExistAsync(string firstName, string lastName);

        public Task<string> GetUserEmailbyIdAsync(int id);
    }
}
