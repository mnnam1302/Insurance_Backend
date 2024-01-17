using backend.IRepositories;
using backend.Models;
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
