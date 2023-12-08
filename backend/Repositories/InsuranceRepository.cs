using backend.DTO;
using backend.Models;
using Microsoft.Data.SqlClient;
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

        public async Task<List<Insurance>> GetAllInsurances(int fromAge, int toAge)
        {
            string sql = "EXEC dbo.GetInsurances @fromAge, " +
                          "@toAge";

            IEnumerable<Insurance> result = await _dbContext.Insurances.FromSqlRaw(sql,
                new SqlParameter("@fromAge", fromAge),
                new SqlParameter("@toAge", toAge)).ToListAsync();

            //List<Insurance> insurances = await _dbContext.Insurances.Where(insurance => insurance.FromAge == fromAge && insurance.ToAge == toAge).ToListAsync();
            return (List<Insurance>)result;
        }

        public async Task<Insurance?> GetInsuranceById(int id)
        {
            var insurance = await _dbContext.Insurances.FindAsync(id);
            return insurance;
        }
    }
}
