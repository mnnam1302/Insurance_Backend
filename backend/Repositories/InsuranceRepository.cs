using backend.DTO.Insurance;
using backend.IRepositories;
using backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace backend.Repositories
{
    public class InsuranceRepository : GenericRepository<Insurance>, IInsuranceRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public InsuranceRepository(InsuranceDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AgeDTO>> GetAllAges()
        {
            try
            {
                List<AgeDTO> uniqueAges = await _dbContext.Insurances
                .Select(insurance => new AgeDTO { FromAge = insurance.FromAge, ToAge = insurance.ToAge })
                .Distinct()
                .ToListAsync();

                return uniqueAges;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Insurance>> GetAllInsurances(int fromAge, int toAge)
        {
            try
            {
                List<Insurance> insurances = await _dbContext.Insurances
                .Where(ins => (fromAge <= ins.FromAge && ins.ToAge <= toAge)).ToListAsync();

                return insurances;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Insurance>> GetInsurancesByAgeCustomer(int age)
        {
            List<Insurance> result = await _dbContext.Insurances.
                Where(ins => (ins.FromAge <= age && age <= ins.ToAge))
                .ToListAsync();

            return result;
        }

        //public async Task<Insurance?> GetInsuranceById(int id)
        //{
        //    var insurance = await _dbContext.Insurances.FindAsync(id);
        //    return insurance;
        //}   
    }
}
