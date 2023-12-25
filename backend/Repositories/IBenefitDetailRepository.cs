using backend.DTO;
using backend.Models;

namespace backend.Repositories
{
    public interface IBenefitDetailRepository
    {
        Task<List<BenefitDetailDTO>> GetBenefitDetailByInsuranceId(int insuranceId);
    }
}
