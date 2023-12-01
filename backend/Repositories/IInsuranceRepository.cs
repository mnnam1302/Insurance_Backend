using backend.Models;

namespace backend.Repositories
{
    public interface IInsuranceRepository
    {
        Task<List<Insurance>> GetAllInsurances();
        Task<Insurance?> GetInsuranceById(int id);
    }
}
