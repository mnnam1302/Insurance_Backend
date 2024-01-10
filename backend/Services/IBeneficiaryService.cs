using backend.DTO.Beneficiary;
using backend.Models;
using backend.Responses;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services
{
    public interface IBeneficiaryService
    {
        Task<BeneficiaryDTO?> GetBeneficiaryById(int beneficiaryId);
        Task<BaseCommandResponse> CreateBeneficiary(CreateBeneficiaryDTO beneficiaryDTO);
    }
}
