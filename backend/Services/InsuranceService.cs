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
