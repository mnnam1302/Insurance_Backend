using backend.DTO.PaymentRequest;
using backend.IRepositories;
using backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class PaymentRequestReponsitory : GenericRepository<PaymentRequest>, IPaymentRequestRepository
    {
        private readonly InsuranceDbContext _context;
        public PaymentRequestReponsitory(InsuranceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaymentRequest?> CreatePaymentRequest(CreatePaymentRequestDTO dto)
        {
            try
            {
                string sql = "exec AddPaymentRequest " +
                    "@contract_id, " +
                    "@total_cost, " +
                    "@description, " +
                    "@image";

                IEnumerable<PaymentRequest?> result = await _context.PaymentRequests.FromSqlRaw(sql,
                    new SqlParameter("@contract_id", dto.ContractId),
                    new SqlParameter("@total_cost", dto.TotalCost),
                    new SqlParameter("@description", dto.Description),
                    new SqlParameter("@image", dto.ImagePaymentRequestUrl)
                    ).ToListAsync();

                var request = result.FirstOrDefault();
                return request;
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
                paymentRequest.RequestStatus = updatePaymentRequestDTO.Status;

                _context.Entry(paymentRequest).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return paymentRequest;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
