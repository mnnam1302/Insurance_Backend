using AutoMapper;
using backend.DTO.Beneficiary;
using backend.DTO.Insurance;
using backend.DTO.InsuranceType;
using backend.DTO.Registration;
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

            CreateMap<InsuranceTypeDTO, InsuranceType>().ReverseMap();

            CreateMap<Insurance, InsuranceDTO>()
                .ForMember(dest => dest.PriceDiscount, opt => opt.MapFrom(src => src.Price * ((100 - src.Discount) / 100)))
                .ReverseMap();

            CreateMap<BeneficiaryDTO, Beneficiary>().ReverseMap();
            CreateMap<CreateBeneficiaryDTO, Beneficiary>().ReverseMap();

            // cái này ok
            CreateMap<RegistrationDTO, Registration>().ReverseMap();

            // Cái này có vấn đề
            CreateMap<CreateRegistrationDTO, Registration>().ReverseMap();
        }
    }
}
