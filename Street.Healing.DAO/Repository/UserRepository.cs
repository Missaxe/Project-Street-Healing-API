using Microsoft.EntityFrameworkCore;
using Street.Healing.Business.Core.Core.Repository;
using Street.Healing.DAO.Context;
using Street.Healing.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.DAO.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userContext;


        public UserRepository(UserDbContext userContext)
        {

            _userContext = userContext;
        }

        /// <summary>
        /// Adding and Saving Users into databse
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        public async Task AddUserAsync(User userObj)
        {
            await _userContext.AddAsync(userObj);
            await _userContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get matching user from db
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(string userEmail)

           => await _userContext.Users.FirstOrDefaultAsync(x => x.Email == userEmail);

        /// <summary>
        /// Get matching user email from db based on Id
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        public async Task<string> GetUserEmailbyIdAsync(int id)

           => await _userContext.Users.Where(x => x.Id == id).Select(x => x.Email).FirstOrDefaultAsync();





        /// <summary>
        /// Check if the email already exist 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> CheckEmailExistAsync(string email)
            => await _userContext.Users.AnyAsync(x => x.Email == email);

        /// <summary>
        /// check if the username already exist
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> CheckUsernameExistAsync(string firstName, string lastName)
            => await _userContext.Users.AnyAsync(x => x.FirstName == firstName);
    }
}
