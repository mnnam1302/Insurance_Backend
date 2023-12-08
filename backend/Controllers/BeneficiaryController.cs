using backend.Attribute;
using backend.DTO;
using backend.Extensions;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BeneficiaryController: ControllerBase
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IUserService _userService;
        private readonly FirebaseController _firebaseController;

        public BeneficiaryController(IBeneficiaryService beneficiaryService, 
                                        IUserService userService,
                                        FirebaseController firebaseController)
        {
            _userService = userService;
            _beneficiaryService = beneficiaryService;
            _firebaseController = firebaseController;

        }


        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> CreateBeneficiary([FromForm] BeneficiaryDTO beneficiaryDTO)
        {
            if (beneficiaryDTO == null)
            {
                return BadRequest("Invalid client request");
            }
            try
            {
                // Kiểm tra người mua có tồn tại không
                int userId = HttpContext.GetUserId();
                var user = await _userService.GetUserById(userId);

                if (user == null)
                {
                    return NotFound("Policyholder is not found");
                }

                // Tạo người thụ hưởng
                beneficiaryDTO.UserId = userId;

                // Nếu có image, xử lý chứng minh nhân dân của người thụ hưởng
                if (beneficiaryDTO.ImageIdentification is not null)
                {
                    IActionResult response = await _firebaseController.UploadImage(beneficiaryDTO.ImageIdentification);

                    if (response is OkObjectResult okResult)
                    {
                        // Phương thức UploadImage trả về dữ liệu thành công, lấy dữ liệu từ okResult.Value
                        string imageUrl = okResult.Value.ToString();
                        beneficiaryDTO.ImageUrl = imageUrl;
                    }
                }

                Beneficiary? beneficary = await _beneficiaryService.CreateBeneficiary(beneficiaryDTO);

                if (beneficary == null)
                {
                    return BadRequest("Beneficiary is not created. Please, check information");
                }

                // Chuyển đổi Beneficiary thành BeneficiaryDTO. Vì trong Beneficiary có thông tin user mà không muốn trả về
                var resultBeneficaryDTO = new BeneficiaryDTO
                {
                    BeneficiaryId = beneficary.BeneficiaryId,
                    Email = beneficary.Email,
                    FullName = beneficary.FullName,
                    Phone = beneficary.Phone,
                    Sex = beneficary.Sex,
                    DateOfBirth = beneficary.DateOfBirth,
                    CardIdentification = beneficary.CardIdentification,
                    ImageUrl = beneficary.ImageIdentificationUrl,
                    Address = beneficary.Address,
                    RelationshipPolicyholder = beneficary.RelationshipPolicyholder,
                    UserId = beneficary.UserId
                };

                return Ok(resultBeneficaryDTO);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }

    }
}
