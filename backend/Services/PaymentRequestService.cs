using AutoMapper;
using backend.DTO;
using backend.DTO.PaymentRequest;
using backend.DTO.Registration;
using backend.IRepositories;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private readonly IPaymentRequestRepository _payment;
        private readonly IMapper _mapper;

        public PaymentRequestService(IPaymentRequestRepository payment,
                                    IMapper mapper)
        {
            _payment = payment;
            _mapper = mapper;
        }

        public async Task<List<PaymentRequestDTO>> GetAll()
        {
            var result = await _payment.GetAll();

            var response = _mapper.Map<List<PaymentRequestDTO>>(result);
            return response;
        }

        public async Task<PaymentRequestDTO?> GetById(int id)
        {
            var result = await _payment.Get(id);

            var response = _mapper.Map<PaymentRequestDTO>(result);
            return response;
        }

        public async Task<PaymentRequest?> CreatePaymentRequest(CreatePaymentRequestDTO dto)
        {
            return await _payment.CreatePaymentRequest(dto);
        }
        public async Task<PaymentRequestDTO?> UpdatePaymentRequest(int id, UpdatePaymentRequestDTO updatePaymentRequestDTO)
        {
            var paymentRequest = await _payment.Get(id);
            var result = await _payment.UpdatePaymentRequest(paymentRequest, updatePaymentRequestDTO);

            var response = _mapper.Map<PaymentRequestDTO>(result);
            return response;
        }
    }
}
