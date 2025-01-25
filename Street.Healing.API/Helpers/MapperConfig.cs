using AutoMapper;
using Street.Healing.API.Context;
using Street.Healing.API.RequestsDto.User;
using static Street.Healing.API.Services.PasswordServices;

namespace Street.Healing.API.Helpers
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            // Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                // Configuring UserClient and User mapping
                cfg.CreateMap<UserClientDto, User>()
                    .ForMember(dest => dest.FirstName, act => act.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.LastName))
                    .ForMember(dest => dest.PhoneNumber, act => act.MapFrom(src => src.PhoneNumber))
                    .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                    .ForMember(dest => dest.DateCreated, act => act.MapFrom(src => src.DateCreated))
                    .AfterMap((src, dest) =>
                    {
                        // Generate hash and salt once and assign to respective properties
                        var hashSalt = GenerateSaltedHash(64, src.Password);
                        dest.HashPassword = hashSalt.Hash;
                        dest.SaltPassword = hashSalt.Salt;
                    });
            });

            // Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }

    }
}
