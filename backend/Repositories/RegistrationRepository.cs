using backend.IRepositories;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class RegistrationRepository : GenericRepository<Registration>, IRegistrationRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public RegistrationRepository(InsuranceDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Registration?> GetById(int id)
        {
            try
            {
                var registration = await _dbContext.Registrations.FindAsync(id);
                return registration;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Registration> UpdateRegistrationStatus(Registration registration, string status)
        {
            try
            {
                registration.RegistrationStatus = status;
                _dbContext.Entry(registration).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return registration;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<Registration?> CreateRegistrationInsurance(CreateRegistrationDTO registrationDTO)
        //{
        //    try
        //    {
        //        // TODO: CODE
        //        string sql = "EXEC dbo.ResgistrationInsurance @start_Date, " +
        //                       "@end_Date, " +
        //                       "@basicInsuranceFee, " +
        //                       "@discount, " +
        //                       "@totalSupplementalBenefitFee, " +
        //                       "@beneficiary_id, " +
        //                       "@insurance_id";

        //        IEnumerable<Registration?> result = await _dbContext.Registrations.FromSqlRaw(sql,
        //            new SqlParameter("@start_Date", registrationDTO.StartDate),
        //            new SqlParameter("@end_Date", registrationDTO.EndDate),
        //            new SqlParameter("@basicInsuranceFee", registrationDTO.BasicInsuranceFee),
        //            new SqlParameter("@discount", registrationDTO.Discount),
        //            new SqlParameter("@totalSupplementalBenefitFee", registrationDTO.TotalSupplementalBenefitFee),
        //            new SqlParameter("@beneficiary_id", registrationDTO.BeneficiaryId),
        //            new SqlParameter("@insurance_id", registrationDTO.InsuranceId)
        //            ).ToListAsync();

        //        Registration? registration = result.FirstOrDefault();
        //        return registration;
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        throw new ArgumentException(ex.Message);
        //    }
        //}

        //public async Task<Registration?> UpdateRegistrationStatus (int registrationId, string status)
        //{
        //    try
        //    {
        //        string sql = "exec UpdateRegistrationStatus " +
        //            "@id, " +
        //            "@status";

        //        IEnumerable<Registration?> result = await _dbContext.Registrations.FromSqlRaw(sql,
        //            new SqlParameter("@id", registrationId),
        //            new SqlParameter("@status", status)
        //            ).ToListAsync();

        //        Registration? registration = result.FirstOrDefault();
        //        return registration;
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        throw new ArgumentException(ex.Message);
        //    }
        //}
    }
}