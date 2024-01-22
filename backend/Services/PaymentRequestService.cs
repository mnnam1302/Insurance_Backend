using AutoMapper;
using backend.DTO;
using backend.DTO.PaymentRequest;
using backend.DTO.Registration;
using backend.DTO.User;
using backend.IRepositories;
using backend.Models;
using backend.Repositories;
using backend.Responses;
using Firebase.Auth;
using Newtonsoft.Json;

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

        public async Task<BaseCommandResponse> CreatePaymentRequest(CreatePaymentRequestDTO dto)
        {
            var response = new BaseCommandResponse();
            var result = await _payment.CreatePaymentRequest(dto);

            if (result == null)
            {
                response.Success = false;
                response.Message = "Create payment request failed";
                response.Errors = new List<string>() { "Something was wrong while creating payment request" };
            }
            else
            {
                response.Success = true;
                response.Message = "Create payment request successfully";
                response.Id = result.PaymentRequestId;
            }

            return response;
        }
        public async Task<PaymentRequestDTO?> UpdatePaymentRequest(int id, UpdatePaymentRequestDTO updatePaymentRequestDTO)
        {
            var paymentRequest = await _payment.Get(id);
            var result = await _payment.UpdatePaymentRequest(paymentRequest, updatePaymentRequestDTO);

            var response = _mapper.Map<PaymentRequestDTO>(result);
            return response;
        }
        public BasePagingResponse<PaymentRequestDTO> GetAllPaging(int page, int pageSize)
        {
            var result = _payment.GetMultiPaging(x => true,
                                                 out int totalRowSelected,
                                                 out int totalRow,
                                                 out int totalPage,
                                                 page,
                                                 pageSize);

            var payment = _mapper.Map<List<PaymentRequestDTO>>(result);

            var response = new BasePagingResponse<PaymentRequestDTO>
            {
                Data = payment,                         // Danh sách payment
                TotalItemSelected = totalRowSelected,   // Số lượng record trả về
                TotalItems = totalRow,                  // Tổng số record trong db
                PageSize = pageSize,                    // Page size
                CurrentPage = page,                     // Current page
                TotalPages = totalPage                  // Total page
            };
            return response;
        }

        public async Task<List<SummaryPaymentRequestDTO>> GetSummaryPaymentRequest(int year)
        {
            var result = await _payment.GetSummaryPaymentRequest(year);

            var response =  _mapper.Map<List<SummaryPaymentRequestDTO>>(result);
            return response;
        }
    }
}
