using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repositories
{
    public interface IBeneficiaryRepository
    {
        Task<Beneficiary?> GetBeneficiaryById(int beneficiaryId);
        Task<Beneficiary?> CreateBeneficiary(BeneficiaryDTO beneficiaryDTO);
    }
}
