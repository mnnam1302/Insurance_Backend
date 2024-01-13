using backend.DTO.Registration;
using backend.Models;

namespace backend.IRepositories
{
    public interface IRegistrationRepository: IGenericRepository<Registration>
    {
        //Task<Registration?> CreateRegistrationInsurance(CreateRegistrationDTO registrationDTO);

        Task<Registration?> GetById(int id);

        //Task<Registration?> UpdateRegistrationStatus(int registrationId, string status);
        Task<Registration> UpdateRegistrationStatus(Registration registration, string status);

    }
}
