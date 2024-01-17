using backend.Models;

namespace backend.IRepositories
{
    public interface IContractPaymentHistoryRepository : IGenericRepository<ContractPaymentHistory>
    {
        Task<ContractPaymentHistory> UpdateStatusContractPayment(ContractPaymentHistory contractPayment, string status);
    }
}
