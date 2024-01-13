using backend.Attribute;
using backend.DTO;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentRequestController : ControllerBase
    {
        private readonly IPaymentRequestService _payment;
        private readonly FirebaseController _firebaseController;

        public PaymentRequestController(IPaymentRequestService payment,
                                        FirebaseController firebaseController)
        {
            _payment = payment;
            _firebaseController = firebaseController;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                List<PaymentRequest> order = await _payment.GetAll();
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                PaymentRequest? order = await _payment.GetById(id);

                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost]
        //[JwtAuthorize]
        public async Task<IActionResult> AddPaymentRequest([FromForm] PaymentRequestDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("invalid request");
            }
            try
            {
                if (dto.ImageIdentification is not null)
                {
                    IActionResult response = await _firebaseController.UploadImage(dto.ImageIdentification);

                    if (response is OkObjectResult okResult)
                    {
                        // Phương thức UploadImage trả về dữ liệu thành công, lấy dữ liệu từ okResult.Value
                        string imageUrl = okResult.Value.ToString();
                        dto.image_identification_url = imageUrl;
                    }
                }

                PaymentRequest? request = await _payment.AddPaymentRequest(dto);

                if (request == null)
                {
                    return BadRequest("Request not created, Please check your request!");
                }

                var r_dto = new PaymentRequestDTO
                {
                    paymentrequest_id = request.paymentrequest_id,
                    total_cost = request.total_cost,
                    total_payment = request.total_payment,
                    Description = request.Description,
                    image_identification_url = request.image_identification_url,
                    Status = request.Status,
                    contract_id = request.contract_id,
                    update_date = request.update_date,
                };

                return Ok(r_dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentRequest([FromRoute] int id, [FromForm] double payment, [FromForm] string status)
        {
            if (payment < 0 || status == null)
            {
                return BadRequest("Please enter your update value!");
            }

            PaymentRequest? request = await _payment.UpdatePaymentRequest(id, payment, status);

            if (request == null)
            {
                return BadRequest("Your update request has not been fulfilled, please check your input!");
            }

            var r_dto = new PaymentRequestDTO
            {
                paymentrequest_id = request.paymentrequest_id,
                total_cost = request.total_cost,
                total_payment = request.total_payment,
                Description = request.Description,
                image_identification_url = request.image_identification_url,
                Status = request.Status,
                contract_id = request.contract_id,
                update_date = request.update_date,
            };

            return Ok(r_dto);
        }
    }
}
