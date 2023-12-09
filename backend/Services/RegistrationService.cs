using backend.DTO;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Services
{
    public class RegistrationService: IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly IInsuranceRepository _insuranceRepository;

        public RegistrationService(IRegistrationRepository registrationRepository,
                                    IBeneficiaryRepository beneficiaryRepository,
                                    IInsuranceRepository insuranceRepository)
        {
            _registrationRepository = registrationRepository;
            _beneficiaryRepository = beneficiaryRepository;
            _insuranceRepository = insuranceRepository;
        }

        public async Task<RegistrationDTO?> CreateRegistrationInsurance(RegistrationDTO registrationDTO)
        {
            // Kiểm tra beneficiary
            Beneficiary? beneficiary = await _beneficiaryRepository.GetBeneficiaryById(registrationDTO.BeneficiaryId);

            if (beneficiary == null)
            {
                throw new ArgumentException("Beneficiary í not valid");
            }
            // Tính tuổi người thụ hưởng
            int beneficiaryAge = DateTime.Now.Year - beneficiary.DateOfBirth.Value.Year;

            // Kiểm tra gói bảo hiểm tồn tại
            Insurance? insurance = await _insuranceRepository.GetInsuranceById(registrationDTO.InsuranceId);

            if (insurance == null)
            {
                throw new ArgumentException("Insurance package is not found");
            }   
            
            int fromAge = insurance.FromAge;
            int toAge = insurance.ToAge;

            // Kiểm tra tuổi người thụ hưởng có hợp lệ với gói bảo hiểm
            if (fromAge <= beneficiaryAge && beneficiaryAge <= toAge)
            {
                // Lưu đơn đăng ký xuống db
                Registration? registration = await _registrationRepository.CreateRegistrationInsurance(registrationDTO);
                //return registration;

                if (registration == null)
                {
                    throw new ArgumentException("Registration is not success");
                }

                RegistrationDTO registrationResponse = new RegistrationDTO
                {
                    RegistrationId = registration.RegistrationId,
                    RegistrationDate = registration.RegistrationDate,
                    StartDate = registration.StartDate,
                    EndDate = registration.EndDate,
                    BasicInsuranceFee = registration.BasicInsuranceFee,
                    Discount = registration.Discount,
                    TotalSupplementalBenefitFee = registration.TotalSupplementalBenefitFee,
                    RegistrationStatus = registration.RegistrationStatus,
                    BeneficiaryId = registration.BeneficiaryId,
                    InsuranceId = registration.InsuranceId
                };

                return registrationResponse;
                
            }
            // Tuổi người thụ hưởng nằm ngoài gói bảo hiểm
            throw new ArgumentException("Beneficiary's age is not correct. Let's check information again");
        }
    }
}
