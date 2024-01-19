using backend.DTO.Beneficiary;
using backend.IRepositories;
using backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace backend.Repositories
{
    public class BeneficiaryRepository: GenericRepository<Beneficiary>, IBeneficiaryRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public BeneficiaryRepository(InsuranceDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BeneficiaryCount>> SummaryBeneficiary()
        {
            try
            {
                var result = await _dbContext.BeneficiaryCounts.ToListAsync();
                return result;


            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<Beneficiary?> GetBeneficiaryById(int beneficiaryId)
        //{
        //    try
        //    {
        //        var beneficiary = await _dbContext.Beneficiaries.FindAsync(beneficiaryId);
        //        return beneficiary;
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        throw new ArgumentException(ex.Message);
        //    }
        //}

        //public async Task<Beneficiary?> CreateBeneficiary(BeneficiaryDTO beneficiaryDTO)
        //{
        //    try
        //    {
        //        // TODO: CODE
        //        string sql = "EXEC dbo.CreateBeneficiary @email, " +
        //                       "@full_name, " +
        //                       "@phone, " +
        //                       "@sex, " +
        //                       "@date_of_birth, " +
        //                       "@card_identification, " +
        //                       "@image_identification_url, " +
        //                       "@address, " +
        //                       "@relationship_policyholder, " +
        //                       "@user_id";

        //        IEnumerable<Beneficiary?> result = await _dbContext.Beneficiaries.FromSqlRaw(sql,
        //            new SqlParameter("@email", beneficiaryDTO.Email ?? ""),
        //            new SqlParameter("@full_name", beneficiaryDTO.FullName),
        //            new SqlParameter("@phone", beneficiaryDTO.Phone ?? ""),
        //            new SqlParameter("@sex", beneficiaryDTO.Sex ?? ""),
        //            new SqlParameter("@date_of_birth", beneficiaryDTO.DateOfBirth),
        //            new SqlParameter("@card_identification", beneficiaryDTO.CardIdentification),
        //            new SqlParameter("@image_identification_url", beneficiaryDTO.ImageIdentificationUrl ?? ""),
        //            new SqlParameter("@address", beneficiaryDTO.Address ?? ""),
        //            new SqlParameter("@relationship_policyholder", beneficiaryDTO.RelationshipPolicyholder ?? ""),
        //            new SqlParameter("@user_id", beneficiaryDTO.UserId)).ToListAsync();

        //        Beneficiary? beneficiary = result.FirstOrDefault();
        //        return beneficiary;
        //    }
        //    catch (ArgumentException ex) {
        //        throw new ArgumentException(ex.Message);
        //    }
        //}
    }
}
