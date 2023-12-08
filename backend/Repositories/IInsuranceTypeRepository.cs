using backend.Models;


namespace backend.Repositories
{
    public interface IInsuranceTypeRepository
    {
        Task<List<InsuranceType>> GetAllInsuranceTypes();
        Task<InsuranceType?> GetInsuranceTypeById(int id);
    }
}
