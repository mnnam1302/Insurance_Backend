using backend.DTO;
using backend.Models;
using backend.Repositories;

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

        public async Task<List<Insurance>> GetInsurancesByAgeCustomer(int age)
        {
            return await _insuranceRepository.GetInsurancesByAgeCustomer(age);
        }

        public async Task<List<Insurance>> GetAllInsurances(int fromAge, int toAge)
        {
            return await _insuranceRepository.GetAllInsurances(fromAge, toAge);
        }

        public async Task<Insurance?> GetInsuranceById(int id)
        {
            return await _insuranceRepository.GetInsuranceById(id);
        }
    }
}
