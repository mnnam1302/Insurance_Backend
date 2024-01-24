using backend.DTO.PaymentRequest;
using backend.IRepositories;
using backend.Models;
using backend.Models.Views;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class PaymentRequestReponsitory : GenericRepository<PaymentRequest>, IPaymentRequestRepository
    {
        private readonly InsuranceDbContext _dbContext;
        public PaymentRequestReponsitory(InsuranceDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<PaymentRequest?> CreatePaymentRequest(CreatePaymentRequestDTO dto)
        //{
        //    try
        //    {
        //        string sql = "exec AddPaymentRequest " +
        //            "@contract_id, " +
        //            "@total_cost, " +
        //            "@description, " +
        //            "@image";

        //        IEnumerable<PaymentRequest?> result = await _dbContext.PaymentRequests.FromSqlRaw(sql,
        //            new SqlParameter("@contract_id", dto.ContractId),
        //            new SqlParameter("@total_cost", dto.TotalCost),
        //            new SqlParameter("@description", dto.Description),
        //            new SqlParameter("@image", dto.ImagePaymentRequestUrl)
        //            ).ToListAsync();

        //        var request = result.FirstOrDefault();
        //        return request;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<List<SummaryPaymentRequest>> GetSummaryPaymentRequest(int year)
        {
            try
            {
                var result = await _dbContext.SummaryPaymentRequests
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

        public async Task<PaymentRequest?> UpdatePaymentRequest(PaymentRequest paymentRequest, UpdatePaymentRequestDTO updatePaymentRequestDTO)
        {
            try
            {
                paymentRequest.TotalPayment = updatePaymentRequestDTO.Payment;
                paymentRequest.RequestStatus = "Đã xử lý";

                _dbContext.Entry(paymentRequest).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return paymentRequest;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
