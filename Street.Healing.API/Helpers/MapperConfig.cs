using AutoMapper;
using Street.Healing.API.Context;
using Street.Healing.API.RequestsData;

namespace Street.Healing.API.Helpers
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<UserClient, User>()
                .ForMember(dest => dest.FirstName, act => act.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, act => act.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                .ForMember(dest => dest.DateCreated, act => act.MapFrom(src => src.DateCreated));
            });

            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
