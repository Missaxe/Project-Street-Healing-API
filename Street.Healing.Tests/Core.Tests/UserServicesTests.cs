using Microsoft.EntityFrameworkCore;
using Street.Healing.Business.Core.Core.Services;
using Street.Healing.DAO.Context;
using Street.Healing.DAO.Models;
using Street.Healing.DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.Tests.Core.Tests
{
    public class UserServicesTests :  IClassFixture<UserServicesFixture>
    {

        private readonly UserDbContext _context;
        private readonly UserRepository _userRepository;
        private readonly UserServices _userServices;

        // Constructor gets called once per test class.
        public UserServicesTests(UserServicesFixture fixture)
        {
            _context = fixture.Context;
            _userRepository = new UserRepository(_context);
            _userServices = new UserServices(_userRepository);
        }
        [Fact]
        public void GetAllUsers_WhenCalled_Get_All_Data()
        {

            //Act
            List<User> users = _userServices.GetAllUsersAsync();

            //Assert
            Assert.Equal(2, users.Count);

        }

        [Fact]
        public async Task AddUser_WhenCalled_Add_New_User()
        {


            //Act
            // Use a clean instance of the context to run the test

            var user = new User
            {
                Id = 3,
                FirstName = "Maissa",
                LastName = "Zf",
                PhoneNumber = "123-456-7890",
                Email = "M.Z@gmail.com",
                HashPassword = "RSXc3J04vkuqaSRpATDSJhFENmsUd+OLabKp0PdNv0K3oJz+cZuomz8V9/eXgtPww4a9NlBmShmZd5NHowZJ3avKD+J880DqT8OZnGEffoZJKW7WvjgW8yZhl157QoUnbaM6xDjfXEx0hjlvOufKoxyuej5aprAlx1hqxIKE3wIPu3FCL/7hWLl3Gz3VXs9T+U8WZDetXyXQxNe574Gfw4n9oQ/DLzY/ajU1tQyJC1rI1jENYPPMBhbBuCejzqiwGahJizfkbYYefW7Yo/7mTkA7umu3H1WSlMEwfVax+Rp5vqzrNHxKLvh8MzcQUzakCmY9dyvNQyuPmfRzZAexXw==",
                SaltPassword = "SoLQoHOufkWWFZVVvmS6cdjTLyW0GT3HU0KBEog4EciNbVO5JTahchA/hlHp6kGlthjZZWoqjXelvBA/ECtiYQ==",
                DateCreated = new DateTime(1996, 6, 3, 16, 02, 14)
            };

            await _userServices.AddUserAsync(user);

            // Directly check the database via DbContext to verify the user was added
            var addedUser = await _userRepository
                               .GetDbContext() // Access the DbContext directly from the repository
                               .Users
                               .FirstOrDefaultAsync(u => u.Email == user.Email);

            // Assert: Check if the user was added directly in the database
            Assert.NotNull(addedUser);
            Assert.Equal(user.FirstName, addedUser.FirstName);
            Assert.Equal(user.LastName, addedUser.LastName);
            Assert.Equal(user.Email, addedUser.Email);


        }

        [Fact]
        public async Task Get_User_By_Email_WhenCalled()
        {

            //Act
            var user = await _userServices.GetUserAsync("M.D@gmail.com");

            //Assert
            Assert.Equal("Mohn", user.FirstName);


        }

        [Fact]
        public async Task GetUserEmailbyIdAsync_WhenCalled_Returns_Email()
        {

            //Act
            var userEmail = await _userServices.GetUserEmailbyIdAsync(1);

            //Assert
            Assert.Equal("M.D@gmail.com", userEmail);

        }

        [Fact]
        public async Task Check_Email_Exist_Returns_True_WhenCalled()
        {
            //Act
            var result = await _userServices.CheckIfEmailExistAsync("M.D@gmail.com");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Check_Username_Exist_Returns_True_WhenCalled()
        {
            //Act
            var result = await _userServices.CheckIfUsernameExistAsync("John");

            //Assert
            Assert.True(result);

        }
    }
}
