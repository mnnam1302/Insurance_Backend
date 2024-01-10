using backend.DTO.Registration;
using backend.Models;
using backend.Responses;

namespace backend.Services
{
    public interface IRegistrationService
    {
        Task<BaseCommandResponse> CreateRegistrationInsurance(CreateRegistrationDTO registrationDTO);
    }
}
