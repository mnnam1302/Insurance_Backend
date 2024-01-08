using AutoMapper;
using backend.DTO.Insurance;
using backend.IRepositories;
using backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Services
{
    public class InsuranceService: IInsuranceService
    {
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IMapper _mapper;

        public InsuranceService(IInsuranceRepository insuranceRepository, IMapper mapper)
        {
            _insuranceRepository = insuranceRepository;
            _mapper = mapper;
        }

        public async Task<List<AgeDTO>> GetAllAges()
        {
            return await _insuranceRepository.GetAllAges();
        }

        public async Task<List<InsuranceDTO>> GetInsurancesByAgeCustomer(int age)
        {
            List<Insurance> insurances = await _insuranceRepository.GetInsurancesByAgeCustomer(age);
            
            var result = _mapper.Map<List<InsuranceDTO>>(insurances);
            return result;
        }

        public async Task<List<InsuranceDTO>> GetAllInsurances(int fromAge, int toAge)
        {
            var insurances =   await _insuranceRepository.GetAllInsurances(fromAge, toAge);

            var result = _mapper.Map<List<InsuranceDTO>>(insurances);
            return result;
        }

        public async Task<InsuranceDTO?> GetInsuranceById(int id)
        {
            var insurance = await _insuranceRepository.Get(id);

            var result = _mapper.Map<InsuranceDTO>(insurance);
            return result;
        }
    }
}
