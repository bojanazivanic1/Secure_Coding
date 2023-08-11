using AutoMapper;
using InsecureCode.DTO;
using InsecureCode.Models;

namespace InsecureCode.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterUserDto>().ReverseMap();
        }
    }
}
