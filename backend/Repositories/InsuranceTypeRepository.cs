using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class InsuranceTypeRepository: IInsuranceTypeRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public InsuranceTypeRepository(InsuranceDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<InsuranceType>> GetAllInsuranceTypes()
        {
            var insuranceTypes = await _dbContext.InsuranceTypes.ToListAsync();
            return insuranceTypes;
        }

        public async Task<InsuranceType?> GetInsuranceTypeById(int id)
        {
            var insuranceType = await _dbContext.InsuranceTypes.FindAsync(id);
            return insuranceType;
        }
    }
}
