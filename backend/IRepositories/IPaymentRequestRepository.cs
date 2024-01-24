using backend.DTO.PaymentRequest;
using backend.Models;
using backend.Models.Views;

namespace backend.IRepositories
{
    public interface IPaymentRequestRepository : IGenericRepository<PaymentRequest>
    {
        //Task<PaymentRequest?> CreatePaymentRequest(CreatePaymentRequestDTO dto);
        Task<PaymentRequest?> UpdatePaymentRequest(PaymentRequest paymentRequest, UpdatePaymentRequestDTO updatePaymentRequestDTO);

        Task<List<SummaryPaymentRequest>> GetSummaryPaymentRequest(int year);
    }
}
