using backend.IRepositories;
using backend.Models;
using backend.Models.Views;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ContractPaymentHistoryRepository : GenericRepository<ContractPaymentHistory>, IContractPaymentHistoryRepository
    {
        public readonly InsuranceDbContext _dbContext;

        public ContractPaymentHistoryRepository(InsuranceDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SummaryPaymentContract>> GetSummaryPaymentContract(int year)
        {
            try
            {
                var result = await _dbContext.SummaryPaymentContracts
                    .Where(x => x.Year == year)
                    .OrderBy(x => x.Month)
                    .ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ContractPaymentHistory> UpdateStatusContractPayment(ContractPaymentHistory contractPayment, string status)
        {
            try
            {
                contractPayment.Status = status;
                _dbContext.Entry(contractPayment).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return contractPayment;

            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
