using backend.Attribute;
using backend.DTO.Insurance;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InsurancesController: ControllerBase
    {
        private readonly IInsuranceService _insuranceService;

        public InsurancesController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        [HttpGet("ages")]
        //[JwtAuthorize]
        public async Task<IActionResult> GetAllAges()
        {
            try
            {
                var ages = await _insuranceService.GetAllAges();
                return Ok(ages);
            }
            catch (Exception ex)
            {
                return BadRequest(new {errors = ex.Message });
            }
        }

        [HttpGet]
        [JwtAuthorize]
        public async Task<IActionResult> GetAllInsurances([FromQuery] int fromAge = 1, int toAge = 3) 
        {
            try
            {
                var insurances = await _insuranceService.GetAllInsurances(fromAge, toAge);

                if (insurances == null)
                {
                    return NotFound();
                }

                return Ok(insurances);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filter")]
        //[JwtAuthorize]
        public async Task<IActionResult> GetInsurancesByAgeCustomer([FromQuery] int age)
        {
            try
            {
                var insurances = await _insuranceService.GetInsurancesByAgeCustomer(age);

                if (insurances == null)
                {
                    return NotFound();
                }

                return Ok(insurances);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInsuranceById([FromRoute] int id)
        {
            try
            {
                var insurance = await _insuranceService.GetInsuranceById(id);

                if (insurance == null)
                {
                    return NotFound();
                }

                return Ok(insurance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
