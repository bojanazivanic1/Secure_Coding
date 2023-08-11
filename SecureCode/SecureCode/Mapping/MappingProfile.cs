using AutoMapper;
using SecureCode.DTO;
using SecureCode.Models;

namespace SecureCode.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterUserDto>().ReverseMap();
        }
    }
}
