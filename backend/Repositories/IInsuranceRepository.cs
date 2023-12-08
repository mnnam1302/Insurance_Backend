using backend.Models;

namespace backend.Repositories
{
    public interface IInsuranceRepository
    {
        Task<List<Insurance>> GetAllInsurances(int fromAge, int toAge);
        Task<Insurance?> GetInsuranceById(int id);
    }
}
