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

        /// <summary>
        /// Method help registration insurance package for a beneficiary
        /// Mình đang stop module đáng ký quyền lợi bổ sung. Làm sao mở rộng phải dễ
        /// </summary>
        /// <param name="registrationDTO"></param>
        /// <returns>
        /// Success: return Registration
        /// Fail:
        /// 1. Request is not valid
        /// 2. Beneficiary is not valid
        /// 3. Insuarnce package is not valid
        /// 4. Beneficiary's age is not correct (Ok: fromAge <= Beneficiary's age <= toAge)
        /// 5. Created registration is failed
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateRegistrationInsurance([FromBody] RegistrationDTO registrationDTO)
        {
            if (registrationDTO == null)
            {
                return BadRequest("Request is not valid");
            }
            try
            {
                var registration = await _registrationService.CreateRegistrationInsurance(registrationDTO);

                if (registration == null)
                {
                    return BadRequest("Created registration is failed");
                }
                return Ok(registration);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
