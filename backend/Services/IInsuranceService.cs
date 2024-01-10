using backend.DTO.Insurance;
using backend.Models;

namespace backend.Services
{
    public interface IInsuranceService
    {
        Task<List<AgeDTO>> GetAllAges();

        Task<List<InsuranceDTO>> GetAllInsurances(int fromAge, int toAge);

        Task<List<InsuranceDTO>> GetInsurancesByAgeCustomer(int age);

        Task<InsuranceDTO?> GetInsuranceById(int id);
    }
}
