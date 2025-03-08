using Microsoft.EntityFrameworkCore;
using Street.Healing.DAO.Context;
using Street.Healing.DAO.Models;
using Street.Healing.DAO.Repository;

namespace Street.Healing.Tests.DAO.Tests
{
    public class UserRepositoryTests : IClassFixture<UserRepositoryFixture>
    {

        private readonly UserDbContext _context;
        private readonly UserRepository _userRepository;

        // Constructor gets called once per test class.
        public UserRepositoryTests(UserRepositoryFixture fixture)
        {
            _context = fixture.Context;
            _userRepository = new UserRepository(_context);
        }


        [Fact]
        public void GetAllUsers_WhenCalled_Get_All_Data()
        {

            //Act
            List<User> users = _userRepository.GetAllUsersAsync();

            //Assert
            Assert.Equal(3, users.Count);

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

            await _userRepository.AddUserAsync(user);

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
            var user = await _userRepository.GetUserAsync("M.D@gmail.com");

            //Assert
            Assert.Equal("Mohn", user.FirstName);


        }

        [Fact]
        public async Task GetUserEmailbyIdAsync_WhenCalled_Returns_Email()
        {

            //Act
            var userEmail = await _userRepository.GetUserEmailbyIdAsync(1);

            //Assert
            Assert.Equal("M.D@gmail.com", userEmail);

        }

        [Fact]
        public async Task Check_Email_Exist_Returns_True_WhenCalled()
        {
            //Act
            var result = await _userRepository.CheckEmailExistAsync("M.D@gmail.com");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Check_Username_Exist_Returns_True_WhenCalled()
        {
            //Act
            var result = await _userRepository.CheckUsernameExistAsync("John");

            //Assert
            Assert.True(result);

        }
    }
}
