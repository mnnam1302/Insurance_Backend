using backend.DTO;
using backend.Models;

namespace backend.Repositories
{
    public interface IContractRepository
    {
        Task<List<Contract>> GetAll();

        Task<Contract?> GetById(int contract_id);

        Task<List<Contract>> GetByUserId(int userId);

        Task<Contract?> AddNewContract(ContractDTO contractDTO);

        Task<Contract?> GetByInsuranceCode(string insurance_code);
    }
}
