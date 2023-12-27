using backend.DTO;
using backend.Models;

namespace backend.Repositories
{
    public interface IRegistrationRepository
    {
        Task<Registration?> CreateRegistrationInsurance(RegistrationDTO registrationDTO);

        Task<Registration?> GetById(int id);

        Task<Registration?> UpdateRegistrationStatus(int registrationId, string status);
    }
}
