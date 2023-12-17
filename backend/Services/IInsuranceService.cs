using backend.DTO;
using backend.Models;

namespace backend.Services
{
    public interface IInsuranceService
    {
        Task<List<AgeDTO>> GetAllAges();
        Task<List<Insurance>> GetAllInsurances(int fromAge, int toAge);

        Task<List<Insurance>> GetInsurancesByAgeCustomer(int age);
        Task<Insurance?> GetInsuranceById(int id);

    }
}
