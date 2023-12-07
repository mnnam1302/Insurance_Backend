using backend.Attribute;
using backend.DTO;
using backend.Extensions;
using backend.Models;
using backend.Services;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BeneficiaryController: ControllerBase
    {
        private readonly BeneficiaryService _beneficiaryService;
        private readonly UserService _userService;

        public BeneficiaryController(BeneficiaryService beneficiaryService,
                                        UserService userService)
        {
            _beneficiaryService = beneficiaryService;
            _userService = userService;
        }


        [HttpPost]
        //[JwtAuthorize]
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

                var beneficary = _beneficiaryService.CreateBeneficiary(beneficiaryDTO);

                if (beneficary == null)
                {
                    return BadRequest("Beneficiary is not created. Please, check information");
                }


                return Ok(beneficary);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }

    }
}
