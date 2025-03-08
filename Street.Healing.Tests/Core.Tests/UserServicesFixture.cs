using Microsoft.EntityFrameworkCore;
using Street.Healing.DAO.Context;
using Street.Healing.DAO.Models;

namespace Street.Healing.Tests.Core.Tests
{
    public class UserServicesFixture:  IDisposable
    {
        public UserDbContext Context { get; private set; }

        public UserServicesFixture()
        {
            // This will create an in-memory database shared across all tests
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: "UserServicesListDatabase")
                .Options;

            Context = new UserDbContext(options);

            // Seed initial data for the entire test class
            Context.Users.Add(new User
            {
                Id = 1,
                FirstName = "Mohn",
                LastName = "Doe",
                PhoneNumber = "123-456-7890",
                Email = "M.D@gmail.com",
                HashPassword = "RSXc3J04vkuqaSRpATDSJhFENmsUd+OLabKp0PdNv0K3oJz+cZuomz8V9/eXgtPww4a9NlBmShmZd5NHowZJ3avKD+J880DqT8OZnGEffoZJKW7WvjgW8yZhl157QoUnbaM6xDjfXEx0hjlvOufKoxyuej5aprAlx1hqxIKE3wIPu3FCL/7hWLl3Gz3VXs9T+U8WZDetXyXQxNe574Gfw4n9oQ/DLzY/ajU1tQyJC1rI1jENYPPMBhbBuCejzqiwGahJizfkbYYefW7Yo/7mTkA7umu3H1WSlMEwfVax+Rp5vqzrNHxKLvh8MzcQUzakCmY9dyvNQyuPmfRzZAexXw==",
                SaltPassword = "SoLQoHOufkWWFZVVvmS6cdjTLyW0GT3HU0KBEog4EciNbVO5JTahchA/hlHp6kGlthjZZWoqjXelvBA/ECtiYQ==",
                DateCreated = new DateTime(2025, 2, 20, 16, 02, 14)
            });

            Context.Users.Add(new User
            {
                Id = 2,
                FirstName = "John",
                LastName = "Snow",
                PhoneNumber = "123-456-7890",
                Email = "J.S@gmail.com",
                HashPassword = "RSXc3J04vkuqaSRpATDSJhFENmsUd+OLabKp0PdNv0K3oJz+cZuomz8V9/eXgtPww4a9NlBmShmZd5NHowZJ3avKD+J880DqT8OZnGEffoZJKW7WvjgW8yZhl157QoUnbaM6xDjfXEx0hjlvOufKoxyuej5aprAlx1hqxIKE3wIPu3FCL/7hWLl3Gz3VXs9T+U8WZDetXyXQxNe574Gfw4n9oQ/DLzY/ajU1tQyJC1rI1jENYPPMBhbBuCejzqiwGahJizfkbYYefW7Yo/7mTkA7umu3H1WSlMEwfVax+Rp5vqzrNHxKLvh8MzcQUzakCmY9dyvNQyuPmfRzZAexXw==",
                SaltPassword = "SoLQoHOufkWWFZVVvmS6cdjTLyW0GT3HU0KBEog4EciNbVO5JTahchA/hlHp6kGlthjZZWoqjXelvBA/ECtiYQ==",
                DateCreated = new DateTime(2022, 2, 22, 22, 22, 22)
            });

            Context.SaveChanges();
        }

        // Dispose of the database context after the tests are done
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
