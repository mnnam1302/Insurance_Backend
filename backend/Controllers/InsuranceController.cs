using backend.Attribute;
using backend.DTO;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InsuranceController: ControllerBase
    {
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        [HttpGet("ages")]
        public async Task<IActionResult> GetAllAges()
        {
            try
            {
                List<AgeDTO> ages = await _insuranceService.GetAllAges();

                if (ages == null)
                {
                    return NotFound();
                }

                return Ok(ages);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInsurances(int fromAge = 1, int toAge = 3) 
        {
            try
            {
                List<Insurance> insurances = await _insuranceService.GetAllInsurances(fromAge, toAge);

                if (insurances == null)
                {
                    return NotFound();
                }

                return Ok(insurances);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInsuranceById([FromRoute] int id)
        {
            try
            {
                Insurance? insurance = await _insuranceService.GetInsuranceById(id);

                if (insurance == null)
                {
                    return NotFound();
                }

                return Ok(insurance);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {Error = ex.Message});
            }
        }
    }
}
