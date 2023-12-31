using backend.DTO;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Services
{
    public class InsuranceService: IInsuranceService
    {
        private readonly IInsuranceRepository _insuranceRepository;

        public InsuranceService(IInsuranceRepository insuranceRepository)
        {
            _insuranceRepository = insuranceRepository;
        }

        public async Task<List<AgeDTO>> GetAllAges()
        {
            return await _insuranceRepository.GetAllAges();
        }

        public async Task<List<InsuranceDTO>> GetInsurancesByAgeCustomer(int age)
        {
            List<Insurance> insurances = await _insuranceRepository.GetInsurancesByAgeCustomer(age);
            List<InsuranceDTO> result = new List<InsuranceDTO>();

            foreach (var item in insurances)
            {

                InsuranceDTO insuranceDTO = new InsuranceDTO
                {
                    InsuranceId = item.InsuranceId,
                    NameInsurance = item.NameInsurance,
                    FromAge = item.FromAge,
                    ToAge = item.ToAge,
                    Price = item.Price,
                    Discount = item.Discount,
                    // Tính giá khuyến mãi - Business
                    PriceDiscount = item.Price * ((100 - item.Discount) / 100),
                    Status = item.Status,
                    InsuranceTypeId = item.InsuranceTypeId,
                };

                result.Add(insuranceDTO);
            }
            return result;
        }

        public async Task<List<InsuranceDTO>> GetAllInsurances(int fromAge, int toAge)
        {
            List<Insurance> insurances =   await _insuranceRepository.GetAllInsurances(fromAge, toAge);
            List<InsuranceDTO> result = new List<InsuranceDTO>();

            foreach(var item in insurances)
            {

                InsuranceDTO insuranceDTO = new InsuranceDTO
                {
                    InsuranceId = item.InsuranceId,
                    NameInsurance = item.NameInsurance,
                    FromAge = item.FromAge,
                    ToAge = item.ToAge,
                    Price = item.Price,
                    Discount = item.Discount,
                    // Tính giá khuyến mãi - Business
                    PriceDiscount = item.Price * ((100 - item.Discount) / 100),
                    Status = item.Status,
                    InsuranceTypeId = item.InsuranceTypeId,
                };

                result.Add(insuranceDTO);
            }
            return result;
        }

        public async Task<InsuranceDTO?> GetInsuranceById(int id)
        {
            Insurance? insurance = await _insuranceRepository.GetInsuranceById(id);
            
            InsuranceDTO insuranceDTO = new InsuranceDTO
            {
                InsuranceId = insurance.InsuranceId,
                NameInsurance = insurance.NameInsurance,
                FromAge = insurance.FromAge,
                ToAge = insurance.ToAge,
                Price = insurance.Price,
                Discount = insurance.Discount,
                // Tính giá khuyến mãi - Business
                PriceDiscount = insurance.Price * ((100 - insurance.Discount) / 100),
                Status = insurance.Status,
                InsuranceTypeId = insurance.InsuranceTypeId,
            };

            return insuranceDTO;
        }
    }
}
