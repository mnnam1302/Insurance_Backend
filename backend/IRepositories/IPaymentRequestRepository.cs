using backend.DTO.PaymentRequest;
using backend.Models;

namespace backend.IRepositories
{
    public interface IPaymentRequestRepository : IGenericRepository<PaymentRequest>
    {
        Task<PaymentRequest?> CreatePaymentRequest(CreatePaymentRequestDTO dto);
        Task<PaymentRequest?> UpdatePaymentRequest(PaymentRequest paymentRequest, UpdatePaymentRequestDTO updatePaymentRequestDTO);
    }
}
