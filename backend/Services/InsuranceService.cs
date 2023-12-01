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

        public async Task<List<Insurance>> GetAllInsurances()
        {
            return await _insuranceRepository.GetAllInsurances();
        }

        public async Task<Insurance?> GetInsuranceById(int id)
        {
            return await _insuranceRepository.GetInsuranceById(id);
        }
    }
}
