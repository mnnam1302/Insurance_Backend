using backend.DTO.Vnpay;

namespace backend.Services
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrl(HttpContent content, VnPayRequestModel model);

        Task<VnPaymentResponseModel> PaymentExecute(IQueryCollection collections);
    }
}
