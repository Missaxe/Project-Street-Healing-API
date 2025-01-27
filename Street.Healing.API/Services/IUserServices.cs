﻿using Street.Healing.API.Context.User;

namespace Street.Healing.API.Services
{
    public interface IUserServices
    {
        public  Task AddUserAsync(User userObj);

        public Task<User> GetUserAsync(string userEmail);

        public Task<bool> CheckEmailExistAsync(string email);

        public Task<bool> CheckUsernameExistAsync(string firstName, string lastName);

        public Task<string> GetUserEmailbyIdAsync(int id);
    }
}
