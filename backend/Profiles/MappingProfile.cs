using AutoMapper;
using backend.DTO;
using backend.DTO.Beneficiary;
using backend.DTO.Contract;
using backend.DTO.Insurance;
using backend.DTO.InsuranceType;
using backend.DTO.PaymentContractHistory;
using backend.DTO.PaymentRequest;
using backend.DTO.Registration;
using backend.DTO.User;
using backend.Models;
using backend.Models.Views;
using Microsoft.AspNetCore.Routing.Constraints;

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
            //CreateMap<RegistrationDTO, Registration>();
            CreateMap<Registration, RegistrationDTO>()
                .ForMember(dest => dest.TotalFee, 
                            opt => opt.MapFrom(src => 
                                                src.BasicInsuranceFee - ((src.BasicInsuranceFee * src.Discount) / 100)))
                .ReverseMap();

            // Cái này có vấn đề
            CreateMap<CreateRegistrationDTO, Registration>().ReverseMap();

            CreateMap<ContractDTO, Contract>().ReverseMap();

            CreateMap<PaymentRequestDTO, PaymentRequest>().ReverseMap();

            CreateMap<CreatePaymentContractHistoryDTO, ContractPaymentHistory>();
            CreateMap<UpdatePaymentContractHistoryDTO, ContractPaymentHistory>();
            CreateMap<PaymentContractHistoryDTO, ContractPaymentHistory>().ReverseMap();


            CreateMap<BeneficiaryCount, BeneficiaryCountDTO>()
                .ForMember(dest => dest.X, opt => opt.MapFrom(src => src.Label))
                .ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.Total));

            CreateMap<SummaryPaymentContract, SummaryPaymentContractDTO>()
                .ForMember(dest => dest.X, opt => opt.MapFrom(src => src.Month))
                .ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.Amount));

            CreateMap<SummaryPaymentRequest, SummaryPaymentRequestDTO>()
                .ForMember(dest => dest.X, opt => opt.MapFrom(src => src.Month))
                .ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.Amount));
        }
    }
}
