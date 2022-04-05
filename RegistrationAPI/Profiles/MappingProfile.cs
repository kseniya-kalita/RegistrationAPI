using AutoMapper;
using Registration.Abstractions.Dtos.Responses;
using Registration.Abstractions.Models;

namespace RegistrationAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Company, CompanyViewModel>();
        }
    }
}
