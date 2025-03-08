using AutoMapper;
using Street.Healing.DAO.Models;
using Street.Healing.DTO.Mapping;
using Street.Healing.DTO.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.Tests.DTO.Tests
{
    public class UserMapperTests
    {
        private readonly IMapper _mapper = UserMapperConfig.InitializeAutomapper();

        [Fact]
        public void MapperConfig_WhenCalled_Map_From_UserDTO_To_User()
        {

            //Arange
            UserDTO user = new UserDTO()
            {
                FirstName = "Fatima",
                LastName = "Ait Mastour",
                PhoneNumber = "123-456-7890",
                Email = "F.A@gmail.com",
                Password = "FatomaZin123@",
                ConfirmPassword = "FatomaZin123@",
                IsEmailValid = true,
                DateCreated = new DateTime(1964, 7, 10, 16, 02, 14)


            };

            //Act
            User mappResult = _mapper.Map<User>(user);

            //Assert
            Assert.Equal("Fatima", mappResult.FirstName);
            Assert.Equal("Ait Mastour", mappResult.LastName);


        }
    }
}
