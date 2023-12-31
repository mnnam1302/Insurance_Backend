using backend.Attribute;
using backend.DTO;
using backend.Models;
using backend.Services;
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
        //[JwtAuthorize]
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
        //[JwtAuthorize]
        public async Task<IActionResult> GetAllInsurances([FromQuery]int fromAge = 1, int toAge = 3) 
        {
            try
            {
                List<InsuranceDTO> insurances = await _insuranceService.GetAllInsurances(fromAge, toAge);

                if (insurances == null)
                {
                    return NotFound();
                }

                return Ok(insurances);
            }
            catch (ArgumentException ex)
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
                List<InsuranceDTO> insurances = await _insuranceService.GetInsurancesByAgeCustomer(age);

                if (insurances == null)
                {
                    return NotFound();
                }
                return Ok(insurances);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInsuranceById([FromRoute] int id)
        {
            try
            {
                InsuranceDTO? insurance = await _insuranceService.GetInsuranceById(id);

                if (insurance == null)
                {
                    return NotFound();
                }

                return Ok(insurance);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
