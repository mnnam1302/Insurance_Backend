using backend.DTO.Beneficiary;
using backend.Responses;

namespace backend.Services
{
    public interface IBeneficiaryService
    {
        Task<BeneficiaryDTO?> GetBeneficiaryById(int beneficiaryId);

        Task<BaseCommandResponse> CreateBeneficiary(CreateBeneficiaryDTO beneficiaryDTO);

        Task<List<BeneficiaryCountDTO>> SummaryBeneficiary();
    }
}