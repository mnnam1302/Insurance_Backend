using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class BeneficiaryRepository: IBeneficiaryRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public BeneficiaryRepository(InsuranceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Beneficiary?> CreateBeneficiary(BeneficiaryDTO beneficiaryDTO)
        {
            try
            {
                // TODO: CODE
                string sql = "EXEC dbo.CreateBeneficiary @email, " +
                               "@full_name, " +
                               "@phone, " +
                               "@sex, " +
                               "@date_of_birth, " +
                               "@card_identification, " +
                               "@image_identification_url, " +
                               "@address, " +
                               "@relationship_policyholder, " +
                               "@user_id";

                IEnumerable<Beneficiary?> result = await _dbContext.Beneficiaries.FromSqlRaw(sql,
                    new SqlParameter("@email", beneficiaryDTO.Email ?? ""),
                    new SqlParameter("@full_name", beneficiaryDTO.FullName),
                    new SqlParameter("@phone", beneficiaryDTO.Phone ?? ""),
                    new SqlParameter("@sex", beneficiaryDTO.Sex ?? ""),
                    new SqlParameter("@date_of_birth", beneficiaryDTO.DateOfBirth),
                    new SqlParameter("@card_identification", beneficiaryDTO.CardIdentification),
                    new SqlParameter("@image_identification_url", beneficiaryDTO.ImageUrl ?? ""),
                    new SqlParameter("@address", beneficiaryDTO.Address ?? ""),
                    new SqlParameter("@relationship_policyholder", beneficiaryDTO.RelationshipPolicyholder ?? ""),
                    new SqlParameter("@user_id", beneficiaryDTO.UserId)).ToListAsync();

                Beneficiary? beneficiary = result.FirstOrDefault();
                return beneficiary;
            }
            catch (ArgumentException ex) {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Beneficiary?> GetBeneficiaryById(int beneficiaryId)
        {
            try
            {
                var beneficiary = await _dbContext.Beneficiaries.FindAsync(beneficiaryId);
                return beneficiary;
            }
            catch(ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
