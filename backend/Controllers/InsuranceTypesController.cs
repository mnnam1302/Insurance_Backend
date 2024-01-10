using backend.Attribute;
using backend.DTO;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InsuranceTypesController: ControllerBase
    {
        private readonly IInsuranceTypeService _insuranceType;

        public InsuranceTypesController(IInsuranceTypeService insuranceType)
        {
            _insuranceType = insuranceType;
        }

        [HttpGet]
        //[JwtAuthorize]
        public async Task<IActionResult> GetAllInsuranceTypes()
        {
            try
            {
                var insuranceTypes = await _insuranceType.GetAllInsuranceTypes();
                return Ok(insuranceTypes);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpGet("{id}")]
        //[JwtAuthorize]
        public async Task<IActionResult> GetAllInsuranceType(int id)
        {
            try
            {
                var insuranceType = await _insuranceType.GetInsuranceTypeById(id);

                if (insuranceType == null)
                {
                    return NotFound("Insurance type is not valid.");
                }

                return Ok(insuranceType);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
