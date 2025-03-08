using Microsoft.EntityFrameworkCore;
using Street.Healing.DAO.Context;
using Street.Healing.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.Tests.DAO.Tests
{
    [TestClass]
    internal class UserRepositoryTests
    {
        [TestMethod]
        public async Task AddUserAsync_WhenCalled_Should_AddUser()
        {
            //Arange

            var userContextMock = new Mock<UserDbContext>();
            userContextMock.Setup<DbSet<User>>(x => x.A))
               
        }
    }
}
