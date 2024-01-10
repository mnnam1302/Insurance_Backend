using backend.DTO.User;
using backend.IRepositories;
using backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public UserRepository(InsuranceDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByCardIdentication(string identification)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.CardIdentification == identification);
        }

        public async Task<User?> CreateUser(User user)
        {
            try
            {
                string sql = "EXEC dbo.RegisterUser @email, " +
                           "@password, " +
                           "@full_name, " +
                           "@phone, " +
                           "@sex, " +
                           "@date_of_birth, " +
                           "@card_identification";

                IEnumerable<User> result = await _dbContext.Users.FromSqlRaw(sql,
                    new SqlParameter("@email", user.Email ?? ""),
                    new SqlParameter("@password", user.Password),
                    new SqlParameter("@full_name", user.FullName),
                    new SqlParameter("@phone", user.Phone ?? ""),
                    new SqlParameter("@sex", user.Sex ?? ""),
                    new SqlParameter("@date_of_birth", user.DateOfBirth),
                    new SqlParameter("@card_identification", user.CardIdentification)).ToListAsync();

                var response = result.FirstOrDefault();
                return response;
            }
            catch (ArgumentException ex) {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
