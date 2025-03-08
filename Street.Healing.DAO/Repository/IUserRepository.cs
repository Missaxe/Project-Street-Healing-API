using Street.Healing.DAO.Context;
using Street.Healing.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.Business.Core.Core.Repository
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User userObj);

        public Task<User> GetUserAsync(string userEmail);

        public UserDbContext GetDbContext();
        public List<User> GetAllUsersAsync();

        public Task<bool> CheckEmailExistAsync(string email);

        public Task<bool> CheckUsernameExistAsync(string firstName);

        public Task<string> GetUserEmailbyIdAsync(int id);
    }
}
