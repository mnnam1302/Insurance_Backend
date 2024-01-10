using backend.DTO.InsuranceType;
using backend.Models;

namespace backend.Services
{
    public interface IInsuranceTypeService
    {
        Task<List<InsuranceTypeDTO>> GetAllInsuranceTypes();

        Task<InsuranceTypeDTO?> GetInsuranceTypeById(int id);
    }
}
