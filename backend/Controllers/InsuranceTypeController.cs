using backend.Attribute;
using backend.DTO;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InsuranceTypeController: ControllerBase
    {
        private readonly IInsuranceTypeService _insuranceType;

        public InsuranceTypeController(IInsuranceTypeService insuranceType)
        {
            _insuranceType = insuranceType;
        }

        [HttpGet]
        //[JwtAuthorize]
        public async Task<IActionResult> GetAllInsuranceType()
        {
            try
            {
                List<InsuranceType> insuranceTypes = await _insuranceType.GetAllInsuranceTypes();

                if (insuranceTypes == null)
                {
                    return NotFound();
                }

                return Ok(insuranceTypes); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        //[JwtAuthorize]
        public async Task<IActionResult> GetAllInsuranceType(int id)
        {
            try
            {
                InsuranceType? insuranceType = await _insuranceType.GetInsuranceTypeById(id);

                if (insuranceType == null)
                {
                    return NotFound();
                }

                return Ok(insuranceType);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
