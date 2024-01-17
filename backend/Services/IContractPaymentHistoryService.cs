using backend.DTO.PaymentContractHistory;
using backend.Responses;

namespace backend.Services
{
    public interface IContractPaymentHistoryService
    {
        Task<BaseCommandResponse> CreatePaymentContractHistory(CreatePaymentContractHistoryDTO paymentContractDTO);

        Task<PaymentContractHistoryDTO> UpdateStatusContractPayment(int paymentContractId, string status);

        Task<PaymentContractHistoryDTO> UpdatePaymentContract(UpdatePaymentContractHistoryDTO paymentContract);
    }
}
