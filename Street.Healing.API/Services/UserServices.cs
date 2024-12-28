using Street.Healing.API.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Street.Healing.API.Services
{
    public class UserServices : IUserServices
    {

        private readonly UserDbContext _userContext;


        public UserServices(UserDbContext userContext)
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
        public Task<User> GetUserAsync(string userEmail)

           => _userContext.Users.FirstOrDefaultAsync(x => x.Email == userEmail);


        /// <summary>
        /// Check if the email already exist 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<bool> CheckEmailExistAsync(string email)
            => _userContext.Users.AnyAsync(x => x.Email == email);

        /// <summary>
        /// check if the username already exist
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<bool> CheckUsernameExistAsync(string firstName , string lastName)
            => _userContext.Users.AnyAsync(x => x.FirstName == firstName);
    }
}
