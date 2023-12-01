using backend.Models;

namespace backend.Services
{
    public interface IInsuranceTypeService
    {
        Task<List<InsuranceType>> GetAllInsuranceTypes();

        Task<InsuranceType?> GetInsuranceTypeById(int id);
    }
}
