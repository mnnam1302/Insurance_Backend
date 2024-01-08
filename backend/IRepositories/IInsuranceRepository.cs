using backend.DTO.Insurance;
using backend.Models;

namespace backend.IRepositories
{
    public interface IInsuranceRepository : IGenericRepository<Insurance>
    {
        Task<List<AgeDTO>> GetAllAges();

        Task<List<Insurance>> GetAllInsurances(int fromAge, int toAge);

        Task<List<Insurance>> GetInsurancesByAgeCustomer(int age);

        //Task<Insurance?> GetInsuranceById(int id);
    }
}
