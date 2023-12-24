using backend.DTO;
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

                // Lưu dữ liệu vào database
                string sql = "EXEC dbo.VerificationPassword @otp_code, @expired, @user_id";

                //new SqlParameter("@expired", DateTime.UtcNow.Add(TimeSpan.FromSeconds(90))),
                //DateTime expired = DateTime.Now.AddSeconds(90);

                IEnumerable<VerificationPassword> result = await _dbContext.VerificationPasswords.FromSqlRaw(sql,
                    new SqlParameter("@otp_code", otp_code),
                    new SqlParameter("@expired", DateTime.Now.AddSeconds(90)),
                    new SqlParameter("@user_id", userId)
                    ).ToListAsync();

                var verificationPassword = result.FirstOrDefault();

                return verificationPassword.OTPCode;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
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
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task ResetPassword(int userId, string newPassword)
        {
            try
            {
                string sql = "EXEC dbo.ResetPassword @user_id, @newPassword";
                // ExecuteSqlRawAsync - ERROR
                //Ở phiên bản cũ, ExecuteSqlRawAsync là một phương thức mở rộng của DbSet<TEntity>. Tuy nhiên, trong một số phiên bản mới, nó đã được chuyển sang DatabaseFacade. Do đó, bạn cần gọi ExecuteSqlRawAsync trên đối tượng Database thay vì trực tiếp trên DbSet< TEntity >.
                // Cách này OK - _dbContext.Database.ExecuteSqlRawAsync
                
                await _dbContext.Database.ExecuteSqlRawAsync(sql,
                       new SqlParameter("@user_id", userId),
                        new SqlParameter("@newPassword", newPassword)
                );

                // Cách này trả về dòng dữ liệu dư thừa
                //IEnumerable<User> result = await _dbContext.Users.FromSqlRaw(sql,
                //    new SqlParameter("@user_id", userId),
                //    new SqlParameter("@newPassword", newPassword)
                //).ToListAsync();

                //if (result == null)
                //{
                //    throw new ArgumentException("Something was wrong in database");
                //}
                return;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
