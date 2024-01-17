using backend.DTO.Registration;
using backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RegistrationsController: ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistrationById([FromRoute] int id)
        {
            try
            {
                var result = await _registrationService.GetRegistrationById(id);
                return Ok(result);
            } catch(Exception ex)
            {
                return BadRequest(new {errors = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegistrationInsurance([FromBody] CreateRegistrationDTO registrationDTO)
        {
            if (registrationDTO == null)
            {
                return BadRequest("Request is not valid.");
            }
            try
            {
                var result = await _registrationService.CreateRegistrationInsurance(registrationDTO);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id}/change-status")]
        public async Task<IActionResult> ChangeStatusRegistration([FromRoute] int id, [FromBody] UpdateStatusRegistrationDTO updateStatusRegistrationDTO)
        {
            if (updateStatusRegistrationDTO == null)
            {
                return BadRequest("Request is not valid.");
            }
            try
            {
                var result = await _registrationService.ChangeStatusRegistration(id, updateStatusRegistrationDTO);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
