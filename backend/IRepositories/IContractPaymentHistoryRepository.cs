using backend.Models;
using backend.Models.Views;

namespace backend.IRepositories
{
    public interface IContractPaymentHistoryRepository : IGenericRepository<ContractPaymentHistory>
    {
        Task<ContractPaymentHistory> UpdateStatusContractPayment(ContractPaymentHistory contractPayment, string status);

        Task<List<SummaryPaymentContract>> GetSummaryPaymentContract(int year);
    }
}
