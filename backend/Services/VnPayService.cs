using backend.DTO.Vnpay;
using backend.Helper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace backend.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _config;

        public VnPayService(IConfiguration config)
        {
            _config = config;
        }

        public string CreatePaymentUrl(HttpContext context, VnPayRequestModel model)
        {
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString());
            
            vnpay.AddRequestData("vnp_CreateDate", model.CreateDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _config["VnPay:Locale"]);

            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toán hợp đồng: {model.ContractId} - ứng với Payment contract history: {model.PaymentContractId}");
            vnpay.AddRequestData("vnp_OrderType", "other"); // Lỗi tối thiếu OrderType, nhưng thuộc tính này optional

            vnpay.AddRequestData("vnp_ReturnUrl", _config["VnPay:PaymentBackReturnUrl"]); // URL thông báo kết quả giao dịch

            // PaymentContractId không trùng
            vnpay.AddRequestData("vnp_TxnRef", model.PaymentContractId.ToString()); // Mã tham chiếu của giao dịch

            var paymentUrl = vnpay.CreateRequestUrl(_config["VnPay:BaseUrl"], _config["VnPay:HashSecret"]);
            return paymentUrl;
        }

        public VnPaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var vnpay = new VnPayLibrary();
            
            foreach(var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_PaymentContractId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_BankCode = vnpay.GetResponseData("vnp_BankCode");
            var vnp_BankTranNo = vnpay.GetResponseData("vnp_BankTranNo");
            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");

            // check xem có ai giả HashSecret không/
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);

            if (!checkSignature)
            {
                return new VnPaymentResponseModel
                {
                    Success = false,
                };
            }

            return new VnPaymentResponseModel
            {
                PaymentContractId = (int)vnp_PaymentContractId,
                PaymentMethod = "VnPay",
                Success = true,
                BankCode = vnp_BankCode,
                BankTransactioNo = vnp_BankTranNo,
                OrderDescription = vnp_OrderInfo,
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode
            };
        }
    }
}
