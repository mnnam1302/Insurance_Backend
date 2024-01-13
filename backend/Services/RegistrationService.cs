using AutoMapper;
using backend.DTO.Registration;
using backend.IRepositories;
using backend.Models;
using backend.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Services
{
    public class RegistrationService: IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IMapper _mapper;

        public RegistrationService(IRegistrationRepository registrationRepository,
                                    IBeneficiaryRepository beneficiaryRepository,
                                    IInsuranceRepository insuranceRepository,
                                    IMapper mapper)
        {
            _registrationRepository = registrationRepository;
            _beneficiaryRepository = beneficiaryRepository;
            _insuranceRepository = insuranceRepository;
            _mapper = mapper;
        }

        public async Task<RegistrationDTO> ChangeStatusRegistration(int id, UpdateStatusRegistrationDTO updateRegistration)
        {
            var registration = await _registrationRepository.Get(id);
            var result = await _registrationRepository.UpdateRegistrationStatus(registration, updateRegistration.Status);

            var response = _mapper.Map<RegistrationDTO>(result);
            return response;
        }

        public async Task<BaseCommandResponse> CreateRegistrationInsurance(CreateRegistrationDTO registrationDTO)
        {
            var response = new BaseCommandResponse();

            // Kiểm tra beneficiary
            var beneficiary = await _beneficiaryRepository.Get(registrationDTO.BeneficiaryId);
            if (beneficiary == null)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = new List<string> { "Beneficiary is not valid." };
                return response;
            }

            // Kiểm tra gói bảo hiểm tồn tại
            var insurance = await _insuranceRepository.Get(registrationDTO.InsuranceId);
            if (insurance == null)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = new List<string> { "Insurance is not valid." };
                return response;
            }

            // Kiểm tra tuổi người thụ hưởng có hợp lệ với gói bảo hiểm

            // Tính tuổi người thụ hưởng
            DateTime currentDate = DateTime.Now;
            int beneficiaryAge = DateTime.Now.Year - beneficiary.DateOfBirth.Year;

            if (currentDate.Month < beneficiary.DateOfBirth.Month || (currentDate.Month == beneficiary.DateOfBirth.Month && currentDate.Day < beneficiary.DateOfBirth.Day))
            {
                beneficiaryAge--;
            }

            if (beneficiaryAge < insurance.FromAge || beneficiaryAge > insurance.ToAge)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = new List<string> { "Beneficiary's age is not suit with insurance" };
                //return response;
            }

            // Đang bị vấn đề
            // Mapper - procedure -> OK
            // Hand convert - genericRepository -> OK

            Registration registration = new Registration
            {
                RegistrationDate = DateTime.Now,
                StartDate = registrationDTO.StartDate,
                EndDate = registrationDTO.EndDate,
                BasicInsuranceFee = registrationDTO.BasicInsuranceFee,
                Discount = registrationDTO.Discount,
                TotalSupplementalBenefitFee = registrationDTO.TotalSupplementalBenefitFee,
                RegistrationStatus = "Chờ xử lý",
                BeneficiaryId = registrationDTO.BeneficiaryId,
                InsuranceId = registrationDTO.InsuranceId
            };

            //var registration = _mapper.Map<Registration>(registrationDTO);

            var result = await _registrationRepository.Add(registration);
            //var result = await _registrationRepository.CreateRegistrationInsurance(registrationDTO);

            if (result is null)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = new List<string> { "Creation registration something wrong." };
            }
            else
            {
                response.Success = true;
                response.Message = "Creation successful";
                response.Id = result.RegistrationId;
            }

            return response;
        }
    }
}
