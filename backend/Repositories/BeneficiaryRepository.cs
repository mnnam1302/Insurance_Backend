using backend.DTO;
using backend.Models;

namespace backend.Repositories
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public BeneficiaryRepository(
            InsuranceDbContext context)
        {
            _dbContext = context;
        }


        public Task<Beneficiary> CreateBeneficiary(BeneficiaryDTO beneficiaryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
