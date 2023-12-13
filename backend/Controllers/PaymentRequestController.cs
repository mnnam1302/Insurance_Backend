using backend.DTO;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentRequestController : ControllerBase
    {
        private readonly IPaymentRequestService _payment;
        private readonly IUserService _userService;
        private readonly FirebaseController _firebaseController;

        public PaymentRequestController(IPaymentRequestService payment,
                                        IUserService userService,
                                        FirebaseController firebaseController)
        {
            _payment = payment;
            _userService = userService;
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
        public async Task<IActionResult> AddPaymentRequest([FromBody] PaymentRequestDTO dto)
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
                    Beneficiaries_id = request.Beneficiaries_id,
                    update_date = request.update_date,
                };

                return Ok(r_dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
