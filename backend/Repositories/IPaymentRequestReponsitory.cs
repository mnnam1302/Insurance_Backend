using backend.DTO;
using backend.Models;

namespace backend.Repositories
{
    public interface IPaymentRequestReponsitory
    {
        Task<List<PaymentRequest>> GetAll();
        Task<PaymentRequest?> GetById(int id);
        Task<PaymentRequest?> AddPaymentRequest(PaymentRequestDTO dto);
    }
}
