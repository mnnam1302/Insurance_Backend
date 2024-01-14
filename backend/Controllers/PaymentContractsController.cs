using backend.Attribute;
using backend.DTO.PaymentContract;
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

        public PaymentContractsController(IVnPayService vnPayService, IUserService userService)
        {
            _vnPayService = vnPayService;
            _userService = userService;
        }


        [JwtAuthorize]
        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] CreatePaymentContractDTO payContractDTO)
        {
            var userId = HttpContext.GetUserId();
            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            // Tạo payment contract - Trạng thái - lưu vào database - tạm thời chưa

            // Tạo request gửi lên VnPay
            var request = new VnPayRequestModel
            {
                FullName = user.FullName,
                ContractId = payContractDTO.ContractId,
                Description = $"{user.FullName} {user.Phone}",
                Amount = payContractDTO.Amount,
                CreateDate = DateTime.Now
            };

            // Lấy url này đưa cho frontend redirect qua
            var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, request);

            return Ok(paymentUrl);
        }

        [JwtAuthorize]
        [HttpPost("payment-back")]
        public async Task<IActionResult> PaymentBack()
        {
            var query = HttpContext.Request.Query;
            var response = _vnPayService.PaymentExecute(query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                return BadRequest("Payment failed");
            }

            // cập nhật trạng thái payment contract - tạm thời chưa

            // Thanh toán thành công
            return Ok("Payment success");
        }
    }
}
