using backend.DTO.BenefitDetail;
using backend.Models;

namespace backend.IRepositories
{
    public interface IBenefitDetailRepository
    {
        Task<List<BenefitDetailDTO>> GetBenefitDetailByInsuranceId(int insuranceId);
    }
}
