using backend.DTO;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private readonly IPaymentRequestRepository _payment;

        public PaymentRequestService(IPaymentRequestRepository payment)
        {
            _payment = payment;
        }

        public async Task<List<PaymentRequest>> GetAll()
        {
            return await _payment.GetAll();
        }

        public async Task<PaymentRequest?> GetById(int id)
        {
            return await _payment.GetById(id);
        }

        public async Task<PaymentRequest?> AddPaymentRequest(PaymentRequestDTO dto)
        {
            return await _payment.AddPaymentRequest(dto);
        }
        public async Task<PaymentRequest?> UpdatePaymentRequest(int id, double payment, string status)
        {
            return await _payment.UpdatePaymentRequest(id, payment, status);
        }
    }
}
