using backend.DTO;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BenefitDetailsController: ControllerBase
    {
        private readonly IBenefitDetailService _benefitDetailService;

        public BenefitDetailsController(IBenefitDetailService benefitDetailService)
        {
            _benefitDetailService = benefitDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBenefitDetailByInsuranceId([FromQuery] int insuranceId)
        {
            try
            {
                var result = await _benefitDetailService.GetBenefitDetailByInsuranceId(insuranceId);

                if (result == null)
                {
                    return NotFound("Benefit detail is not found");
                }

                return Ok(result);
               
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
