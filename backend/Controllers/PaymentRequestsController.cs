using backend.Attribute;
using backend.DTO.PaymentRequest;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentRequestsController : ControllerBase
    {
        private readonly IPaymentRequestService _payment;
        private readonly FirebaseController _firebaseController;

        public PaymentRequestsController(IPaymentRequestService payment,
                                        FirebaseController firebaseController)
        {
            _payment = payment;
            _firebaseController = firebaseController;
        }

        /// <summary>
        /// Get all payment request
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPaymentRequests([FromQuery] int page, int pageSize)
        {
            try
            {
                var paymentRequests = _payment.GetAllPaging(page, pageSize);
                return Ok(paymentRequests);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        /// <summary>
        /// Get payment request base on PaymentRequestId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var paymentRequest = await _payment.GetById(id);

                if (paymentRequest == null)
                {
                    return NotFound();
                }

                return Ok(paymentRequest);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        /// <summary>
        /// Create payment request
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        //[JwtAuthorize]
        public async Task<IActionResult> CreatePaymentRequest([FromForm] CreatePaymentRequestDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("invalid request");
            }
            try
            {
                if (dto.ImagePaymentRequest is not null)
                {
                    IActionResult response = await _firebaseController.UploadImage(dto.ImagePaymentRequest);

                    if (response is OkObjectResult okResult)
                    {
                        // Phương thức UploadImage trả về dữ liệu thành công, lấy dữ liệu từ okResult.Value
                        string imageUrl = okResult.Value.ToString();
                        dto.ImagePaymentRequestUrl = imageUrl;
                    }
                }

                var result = await _payment.CreatePaymentRequest(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update payment request { Payment, Status }
        /// </summary>
        /// <param name="id"></param>
        /// <param name="payment"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentRequest([FromRoute] int id, UpdatePaymentRequestDTO updatePaymentRequestDTO)
        {
            if (updatePaymentRequestDTO == null)
            {
                return BadRequest("Request is not valid");
            }
            try
            {
                var result = await _payment.UpdatePaymentRequest(id, updatePaymentRequestDTO);

                return Ok(result);
            } 
            catch (Exception ex) 
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpGet("summary")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetSummaryPaymentRequest([FromQuery] int year)
        {
            try
            {
                var result = await _payment.GetSummaryPaymentRequest(year);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
