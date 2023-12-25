using backend.DTO;
using backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class BenefitDetailRepository : IBenefitDetailRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public BenefitDetailRepository(InsuranceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BenefitDetailDTO>> GetBenefitDetailByInsuranceId(int insuranceId)
        {
           try
           {
                var result = await _dbContext.BenefitDetails
                .Where(bd => bd.InsuranceId == insuranceId)
                .Join(
                    _dbContext.Benefits,
                    bd => bd.BenefitId,
                    b => b.BenefitId,
                    (bd, b) => new {bd, b}
                 )
                .Join(
                    _dbContext.BenefitTypes,
                    temp => temp.b.BenefitTypeId,
                    bt => bt.BenefitTypeId,
                    (temp, bt) => new BenefitDetailDTO {
                                            NameBenefitType = bt.NameBenefitType,
                                            NameBenefit = temp.b.NameBenefit, 
                                            ClaimSettlement = temp.bd.ClaimSettlementFee }
                ).ToListAsync();

                return result;


           } catch (Exception ex)
           {
                throw new ArgumentException(ex.Message);
           }
        }
    }
}
