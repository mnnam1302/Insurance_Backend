using backend.Attribute;
using backend.DTO.PaymentContractHistory;
using backend.DTO.Vnpay;
using backend.Extensions;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentContractsController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IUserService _userService;
        private readonly IContractPaymentHistoryService _contractPaymentHistoryService;

        public PaymentContractsController(IVnPayService vnPayService, 
                                            IUserService userService,
                                            IContractPaymentHistoryService contractPaymentHistoryService)
        {
            _vnPayService = vnPayService;
            _userService = userService;
            _contractPaymentHistoryService = contractPaymentHistoryService;
        }


        [JwtAuthorize]
        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] CreatePaymentContractHistoryDTO paymentContractDTO)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                var user = await _userService.GetUserById(userId);

                if (user == null)
                {
                    return BadRequest("User not found");
                }

                // Tạo payment contract - Trạng thái - lưu vào database - tạm thời chưa
                var paymentContract = await _contractPaymentHistoryService.CreatePaymentContractHistory(paymentContractDTO);

                // Tạo request gửi lên VnPay
                var request = new VnPayRequestModel
                {
                    FullName = user.FullName,
                    PaymentContractId = paymentContract.Id,
                    ContractId = paymentContractDTO.ContractId,
                    Description = $"{user.FullName} {user.Phone}",
                    Amount = paymentContractDTO.PaymentAmount,
                    CreateDate = DateTime.Now
                };

                var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, request);
                return Ok(paymentUrl);
            } catch(Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        //[JwtAuthorize]
        [HttpGet("payment-back")]
        public async Task<IActionResult> PaymentBack()
        {
            try
            {
                var query = HttpContext.Request.Query;
                var response = _vnPayService.PaymentExecute(query);

                var paymentContract = new UpdatePaymentContractHistoryDTO
                {
                    PaymentContractId = response.PaymentContractId,
                    BankName = response.BankCode,
                    TransactionCode = response.BankTransactioNo,
                    ServicePayment = response.PaymentMethod,
                    Status = response.VnPayResponseCode
                };

                if (response == null || response.VnPayResponseCode != "00")
                {
                    // Update status cua paymentContract - Thất bại
                    var result_failed = await _contractPaymentHistoryService.UpdatePaymentContract(paymentContract);
                    return BadRequest("Payment failed");
                }

                // cập nhật trạng thái payment contract - Đã thanh toán
                var result_success = await _contractPaymentHistoryService.UpdatePaymentContract(paymentContract);
                return Ok("Payment success");
            } 
            catch(Exception ex)
            {
                return BadRequest(new {errors = ex.Message });
            } 
        }
    }
}
