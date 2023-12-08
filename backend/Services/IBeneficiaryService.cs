using backend.DTO;
using backend.Models;

namespace backend.Services
{
    public interface IBeneficiaryService
    {
        Task<Beneficiary?> CreateBeneficiary(BeneficiaryDTO beneficiaryDTO);
    }
}
