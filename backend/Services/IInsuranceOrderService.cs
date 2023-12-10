using backend.DTO;
using backend.Models;

namespace backend.Services
{
    public interface IInsuranceOrderService
    {
        Task<List<InsuranceOrder>> GetAll();
        Task<InsuranceOrder?> GetById(int id);
        Task<InsuranceOrder?> AddInsuranceOrder(InsuranceOrderDTO dto);
    }
}
