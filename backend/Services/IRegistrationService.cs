using backend.DTO.Registration;
using backend.Models;

namespace backend.Services
{
    public interface IRegistrationService
    {
        Task<RegistrationDTO?> CreateRegistrationInsurance(RegistrationDTO registrationDTO);
    }
}
