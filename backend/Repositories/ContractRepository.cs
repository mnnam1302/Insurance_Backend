using backend.DTO.Contract;
using backend.IRepositories;
using backend.Models;
using backend.Models.Views;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ContractRepository : GenericRepository<Contract>, IContractRepository
    {
        private readonly InsuranceDbContext _dbContext;

        public ContractRepository(InsuranceDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Contract>> GetContractByUserId(int userId)
        {
            try
            {
                var contracts = await _dbContext.Contracts
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                return contracts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Contract?> GetContractByInsuranceCode(string insurance_code)
        {
            try
            {
                var contract = await _dbContext.Contracts
                    .Where(c => c.InsuranceCode == insurance_code)
                    .ToListAsync();

                var result = contract.FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Contract?> CreateContract(ContractDTO contractDTO)
        {
            try
            {
                string sql = "exec AddContract " +
                                "@start_date ," +
                                "@end_date ," +
                                "@registion_id ," +
                                "@beneficiary_id ," +
                                "@initial_fee , " +
                                "@discount , " +
                                "@total_fee ," +
                                "@periodic_fee ," +
                                "@user_id ," +
                                "@total_turn "; 

                IEnumerable<Contract> result = await _dbContext.Contracts.FromSqlRaw(sql,
                    new SqlParameter("@start_date", contractDTO.StartDate),
                    new SqlParameter("@end_date", contractDTO.EndDate),
                    new SqlParameter("@registion_id", contractDTO.RegistrationId),
                    new SqlParameter("@beneficiary_id", contractDTO.BeneficiayId),
                    new SqlParameter("@initial_fee", contractDTO.InitialFeePerTurn),
                    new SqlParameter("@discount", contractDTO.Discount),
                    new SqlParameter("@total_fee", contractDTO.TotalFee),
                    new SqlParameter("@periodic_fee", contractDTO.PeriodFee),
                    new SqlParameter("@user_id", contractDTO.UserId),
                    new SqlParameter("@total_turn", contractDTO.TotalTurn)).ToListAsync();

                var contract = result.FirstOrDefault();
                return contract;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Contract> UpdateContractStatus(Contract contract, string status)
        {
            try
            {
                contract.ContractStatus = status;
                _dbContext.Entry(contract).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return contract;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ContractRevenue>> GetSummaryContract()
        {
            try
            {
                var result = await _dbContext.ContractRevenues.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
