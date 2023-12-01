using backend.Models;

namespace backend.Services
{
    public interface IInsuranceService
    {
        Task<List<Insurance>> GetAllInsurances();
        Task<Insurance?> GetInsuranceById(int id);
    }
}
