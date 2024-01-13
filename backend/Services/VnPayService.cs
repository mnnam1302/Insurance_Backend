using backend.DTO.Vnpay;

namespace backend.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _config;

        public VnPayService(IConfiguration config)
        {
            _config = config;
        }

        public Task<string> CreatePaymentUrl(HttpContent content, VnPayRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<VnPaymentResponseModel> PaymentExecute(IQueryCollection collections)
        {
            throw new NotImplementedException();
        }
    }
}
