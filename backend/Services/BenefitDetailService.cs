using backend.DTO;
using backend.Repositories;

namespace backend.Services
{
    public class BenefitDetailService: IBenefitDetailService
    {
        private readonly IBenefitDetailRepository _benefitDetailRepository;


        public BenefitDetailService(IBenefitDetailRepository benefitDetailRepository)
        {
            _benefitDetailRepository = benefitDetailRepository;
        }

        public async Task<List<BenefitDetailDTO>> GetBenefitDetailByInsuranceId(int insuranceId)
        {
            return await _benefitDetailRepository.GetBenefitDetailByInsuranceId(insuranceId);
        }
    }
}
