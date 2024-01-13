using backend.DTO.Contract;
using backend.Models;

namespace backend.IRepositories
{
    public interface IContractRepository : IGenericRepository<Contract>
    {
        Task<List<Contract>> GetContractByUserId(int userId);
        Task<Contract?> GetContractByInsuranceCode(string insurance_code);
        Task<Contract?> CreateContract(ContractDTO dto);

    }
}
