using backend.DTO.Registration;
using backend.Models;

namespace backend.IRepositories
{
    public interface IRegistrationRepository
    {
        Task<Registration?> CreateRegistrationInsurance(RegistrationDTO registrationDTO);

        Task<Registration?> GetById(int id);

        Task<Registration?> UpdateRegistrationStatus(int registrationId, string status);
    }
}
