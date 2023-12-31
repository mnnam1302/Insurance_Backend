using backend.DTO;
using backend.Models;

namespace backend.Repositories
{
    public interface IInsuranceRepository
    {
        Task<List<AgeDTO>> GetAllAges();

        Task<List<Insurance>> GetAllInsurances(int fromAge, int toAge);

        Task<List<Insurance>> GetInsurancesByAgeCustomer(int age);

        Task<Insurance?> GetInsuranceById(int id);
    }
}
