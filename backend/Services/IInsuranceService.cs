using backend.Models;

namespace backend.Services
{
    public interface IInsuranceService
    {
        Task<List<Insurance>> GetAllInsurances(int fromAge, int toAge);
        Task<Insurance?> GetInsuranceById(int id);
    }
}
