using backend.DTO.PaymentContractHistory;
using backend.Models.Views;
using backend.Responses;

namespace backend.Services
{
    public interface IContractPaymentHistoryService
    {
        Task<BaseCommandResponse> CreatePaymentContractHistory(CreatePaymentContractHistoryDTO paymentContractDTO);

        Task<PaymentContractHistoryDTO> UpdateStatusContractPayment(int paymentContractId, string status);

        Task<PaymentContractHistoryDTO> UpdatePaymentContract(UpdatePaymentContractHistoryDTO paymentContract);

        Task<List<SummaryPaymentContractDTO>> GetSummaryPaymentContract(int year);
    }
}
