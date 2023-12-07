using backend.DTO;
using backend.Models;

namespace backend.Repositories
{
    public interface IBeneficiaryRepository
    {
        Task<Beneficiary> CreateBeneficiary(BeneficiaryDTO beneficiaryDTO);
    }
}
