using ApiSmartHome.Contracts.Models.Users;
using ApiSmartHome.Data.Models;
using AutoMapper;

namespace ApiSmartHome.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserView>().ConstructUsing(v => new UserView(v));
            
        }
    }
}
