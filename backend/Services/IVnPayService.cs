using backend.DTO.Vnpay;

namespace backend.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPayRequestModel model);

        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
