using backend.Attribute;
using backend.DTO.Beneficiary;
using backend.Extensions;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BeneficiariesController: ControllerBase
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IUserService _userService;
        private readonly FirebaseController _firebaseController;

        public BeneficiariesController(IBeneficiaryService beneficiaryService, 
                                        IUserService userService,
                                        FirebaseController firebaseController)
        {
            _userService = userService;
            _beneficiaryService = beneficiaryService;
            _firebaseController = firebaseController;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeneficiaryById([FromRoute] int id)
        {
            try
            {
                var result = await _beneficiaryService.GetBeneficiaryById(id);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> CreateBeneficiary([FromForm] CreateBeneficiaryDTO beneficiaryDTO)
        {
            if (beneficiaryDTO == null)
            {
                return BadRequest("Invalid client request.");
            }
            try
            {
                // Kiểm tra người mua có tồn tại không
                int userId = HttpContext.GetUserId();
                var user = await _userService.GetUserById(userId);

                if (user == null)
                {
                    return NotFound("Policyholder is not found.");
                }

                // Tạo người thụ hưởng
                beneficiaryDTO.UserId = userId;

                //Nếu có image, xử lý chứng minh nhân dân của người thụ hưởng
                if (beneficiaryDTO.ImageIdentification is not null)
                {
                    IActionResult response = await _firebaseController.UploadImage(beneficiaryDTO.ImageIdentification);

                    if (response is OkObjectResult okResult)
                    {
                        // Phương thức UploadImage trả về dữ liệu thành công, lấy dữ liệu từ okResult.Value
                        string imageUrl = okResult.Value.ToString();
                        beneficiaryDTO.ImageIdentificationUrl = imageUrl;
                    }
                }

                var result = await _beneficiaryService.CreateBeneficiary(beneficiaryDTO);

                return Ok(result);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("summary")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetSummaryBeneficiaryByAge()
        {
            try
            {
                var result = await _beneficiaryService.SummaryBeneficiary();
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
