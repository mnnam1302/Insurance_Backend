using backend.DTO;
using backend.Models;

namespace backend.Repositories
{
    public interface IInsuranceOrderReponsitories
    {
        Task<List<InsuranceOrder>> GetAll();
        Task<InsuranceOrder?> GetById(int id);
        Task<InsuranceOrder?> AddInsuranceOrder(InsuranceOrderDTO dto);
    }
}
