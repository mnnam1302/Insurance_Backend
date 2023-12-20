using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class RegistrationRepository:IRegistrationRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public RegistrationRepository(InsuranceDbContext context)
        {
            _dbContext = context;
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

        public async Task<Registration?> CreateRegistrationInsurance(RegistrationDTO registrationDTO)
        {
            try
            {
                // TODO: CODE
                string sql = "EXEC dbo.ResgistrationInsurance @start_Date, " +
                               "@end_Date, " +
                               "@basicInsuranceFee, " +
                               "@discount, " +
                               "@totalSupplementalBenefitFee, " +
                               "@beneficiary_id, " +
                               "@insurance_id";

                IEnumerable<Registration?> result = await _dbContext.Registrations.FromSqlRaw(sql,
                    new SqlParameter("@start_Date", registrationDTO.StartDate),
                    new SqlParameter("@end_Date", registrationDTO.EndDate),
                    new SqlParameter("@basicInsuranceFee", registrationDTO.BasicInsuranceFee),
                    new SqlParameter("@discount", registrationDTO.Discount),
                    new SqlParameter("@totalSupplementalBenefitFee", registrationDTO.TotalSupplementalBenefitFee),
                    new SqlParameter("@beneficiary_id", registrationDTO.BeneficiaryId),
                    new SqlParameter("@insurance_id", registrationDTO.InsuranceId)
                    ).ToListAsync();

                Registration? registration = result.FirstOrDefault();
                return registration;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
