using backend.DTO;
using backend.IRepositories;
using backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Reflection.Emit;

namespace backend.Repositories
{
    public class VerificationPasswordRepository : IVerificationPasswordRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public VerificationPasswordRepository(InsuranceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GenerateOTP(int userId)
        {
            try
            {
                // Generate 6 chữ số
                Random random = new Random();
                string otp_code = random.Next(100000, 999999).ToString();

                string sql = "EXEC dbo.VerificationPassword @otp_code, @expired, @user_id";

                IEnumerable<VerificationPassword> result = await _dbContext.VerificationPasswords.FromSqlRaw(sql,
                    new SqlParameter("@otp_code", otp_code),
                    new SqlParameter("@expired", DateTime.Now.AddSeconds(90)),
                    new SqlParameter("@user_id", userId)
                    ).ToListAsync();

                var verificationPassword = result.FirstOrDefault();
                return verificationPassword.OTPCode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VerificationPassword?> GetOtpRecentlyByUserId(int userId)
        {
            try
            {
                var result = await _dbContext.VerificationPasswords
                                            .OrderByDescending(v => v.Expired)
                                            .FirstOrDefaultAsync(v => v.UserId == userId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ResetPassword(int userId, string newPassword)
        {
            try
            {
                string sql = "EXEC dbo.ResetPassword @user_id, @newPassword";

                await _dbContext.Database.ExecuteSqlRawAsync(sql,
                       new SqlParameter("@user_id", userId),
                        new SqlParameter("@newPassword", newPassword));
                return;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
