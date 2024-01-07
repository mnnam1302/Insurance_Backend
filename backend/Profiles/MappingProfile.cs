using AutoMapper;
using backend.DTO.User;
using backend.Models;

namespace backend.Profiles
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // TODO: Code
            CreateMap<CreateUserDTO, User>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UpdateUserDTO, User>().ReverseMap();


        }
    }
}
