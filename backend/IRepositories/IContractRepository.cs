using backend.DTO.Contract;
using backend.Models;
using backend.Models.Views;

namespace backend.IRepositories
{
    public interface IContractRepository : IGenericRepository<Contract>
    {
        Task<List<Contract>> GetContractByUserId(int userId);
        Task<Contract?> GetContractByInsuranceCode(string insurance_code);
        Task<Contract?> CreateContract(Contract contact);
        Task<Contract> UpdateContractStatus(Contract contract, string status);

        Task<List<ContractRevenue>> GetSummaryContract();

    }
}
