using backend.DTO;
using backend.Models;

namespace backend.Services
{
    public interface IPaymentRequestService
    {
        Task<List<PaymentRequest>> GetAll();
        Task<PaymentRequest?> GetById(int id);
        Task<PaymentRequest?> AddPaymentRequest(PaymentRequestDTO dto);
        Task<PaymentRequest?> UpdatePaymentRequest(int id, double payment, string status);
    }
}
