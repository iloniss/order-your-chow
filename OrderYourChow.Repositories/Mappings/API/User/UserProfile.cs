using AutoMapper;
using OrderYourChow.CORE.Models.API.User;
using OrderYourChow.DAL.CORE.Models;
namespace OrderYourChow.Repositories.Mappings.API.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, DUser>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(d => d.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(d => d.Sysdate, opt => opt.Ignore())
                .ForMember(d => d.Syslog, opt => opt.Ignore())
                .ReverseMap();
        }
        
    }
}
