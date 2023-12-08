using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class InsuranceTypeService : IInsuranceTypeService
    {
        private readonly IInsuranceTypeRepository _insuranceRepository;

        public InsuranceTypeService(IInsuranceTypeRepository insuranceRepository)
        {
            _insuranceRepository = insuranceRepository;
        }

        public async Task<List<InsuranceType>> GetAllInsuranceTypes()
        {
            return await _insuranceRepository.GetAllInsuranceTypes();
        }

        public async Task<InsuranceType?> GetInsuranceTypeById(int id)
        {
            return await _insuranceRepository.GetInsuranceTypeById(id);
        }
    }
}
