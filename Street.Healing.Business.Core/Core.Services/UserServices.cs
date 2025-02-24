using Street.Healing.Business.Core.Core.Repository;
using Street.Healing.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.Business.Core.Core.Services
{
    public class UserServices(IUserRepository userRepository) : IUserServices
    {
        private readonly IUserRepository _userRepository = userRepository;

        public Task AddUserAsync(User userObj)
        {
            return  _userRepository.AddUserAsync(userObj);
        }

        public Task<bool> CheckIfEmailExistAsync(string email)
        {
            return _userRepository.CheckEmailExistAsync(email) ;
        }

        public Task<bool> CheckIfUsernameExistAsync(string firstName, string lastName)
        {
            return _userRepository.CheckUsernameExistAsync(firstName,lastName);
        }

        public Task<User> GetUserAsync(string userEmail)
        {
            return _userRepository.GetUserAsync(userEmail);
        }

        public Task<string> GetUserEmailbyIdAsync(int id)
        {
            return _userRepository.GetUserEmailbyIdAsync(id);
        }
    }
}
