using backend.DTO;
using backend.DTO.PaymentRequest;
using backend.Models;
using backend.Responses;

namespace backend.Services
{
    public interface IPaymentRequestService
    {
        Task<List<PaymentRequestDTO>> GetAll();
        Task<PaymentRequestDTO?> GetById(int id);
        Task<BaseCommandResponse> CreatePaymentRequest(CreatePaymentRequestDTO dto);
        Task<PaymentRequestDTO?> UpdatePaymentRequest(int id, UpdatePaymentRequestDTO updatePaymentRequestDTO);

        Task<List<SummaryPaymentRequestDTO>> GetSummaryPaymentRequest(int year);
        BasePagingResponse<PaymentRequestDTO> GetAllPaging(int page, int pageSize);
    }
}
