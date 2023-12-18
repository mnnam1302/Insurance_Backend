using backend.DTO;
using backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly InsuranceDbContext _dbContext;
        //private readonly IConfiguration _config;

        //public UserRepository(InsuranceDbContext context, IConfiguration config)
        public UserRepository(InsuranceDbContext context)
        {
            _dbContext = context;
            //_config = config;
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> Create(RegisterDTO registerDTO)
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
                    new SqlParameter("@email", registerDTO.Email ?? ""),
                    new SqlParameter("@password", registerDTO.Password),
                    new SqlParameter("@full_name", registerDTO.FullName),
                    new SqlParameter("@phone", registerDTO.Phone ?? ""),
                    new SqlParameter("@sex", registerDTO.Sex ?? ""),
                    new SqlParameter("@date_of_birth", registerDTO.DateOfBirth),
                    new SqlParameter("@card_identification", registerDTO.CardIdentification)).ToListAsync();

                User? user = result.FirstOrDefault();
                return user;
            }
            catch (ArgumentException ex) {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<User?> UpdateUserById(UserDTO userDTO)
        {
            var userDomain = _dbContext.Users.SingleOrDefault(x => x.UserId == userDTO.UserId);  
            if (userDomain == null) 
            {
                return null;
            }

            userDomain.FullName = userDTO.FullName; 
            userDomain.Phone = userDTO.Phone;
            userDomain.Email = userDTO.Email;   
            userDomain.DateOfBirth = userDTO.DateOfBirth;   
            userDomain.CardIdentification = userDTO.CardIdentification;
            userDomain.Sex = userDTO.Sex;

            await _dbContext.SaveChangesAsync();

            var request = new User
            {
                FullName = userDomain.FullName,
                Phone = userDomain.Phone,
                Email = userDomain.Email,
                DateOfBirth = userDTO.DateOfBirth,
                CardIdentification = userDTO.CardIdentification,
                Sex = userDTO.Sex,
            };

            return request;
        }



    }
}
