using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class InsuranceRepository: IInsuranceRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public InsuranceRepository(InsuranceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Insurance>> GetAllInsurances()
        {
            var insurances = await _dbContext.Insurances.ToListAsync();
            return insurances;
        }

        public async Task<Insurance?> GetInsuranceById(int id)
        {
            var insurance = await _dbContext.Insurances.FindAsync(id);
            return insurance;
        }
    }
}
