using backend.IRepositories;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class InsuranceTypeRepository: GenericRepository<InsuranceType>,  IInsuranceTypeRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public InsuranceTypeRepository(InsuranceDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
