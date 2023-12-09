using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services
{
    public interface IBeneficiaryService
    {
        Task<Beneficiary?> GetBeneficiaryById(int beneficiaryId);
        Task<Beneficiary?> CreateBeneficiary(BeneficiaryDTO beneficiaryDTO);
    }
}
