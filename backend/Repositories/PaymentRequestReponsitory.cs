using backend.DTO;
using backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class PaymentRequestReponsitory : IPaymentRequestReponsitory
    {
        private readonly InsuranceDbContext _context;
        public PaymentRequestReponsitory(InsuranceDbContext context)
        {
            _context = context;
        }
        public async Task<List<PaymentRequest>> GetAll()
        {
            string sql = "select * from payment_request";
            IEnumerable<PaymentRequest> result = await _context.PaymentRequests.FromSqlRaw(sql).ToListAsync();

            return (List<PaymentRequest>)result;
        }

        public async Task<PaymentRequest?> GetById(int id)
        {
            var request = await _context.PaymentRequests.FindAsync(id);
            return request;
        }

        public async Task<PaymentRequest?> AddPaymentRequest(PaymentRequestDTO dto)
        {
            try
            {
                string sql = "exec AddPaymentRequest " +
                    "@beneficiaries_id, " +
                    "@total_cost, " +
                    "@description, " +
                    "@image";

                IEnumerable<PaymentRequest?> result = await _context.PaymentRequests.FromSqlRaw(sql,
                    new SqlParameter("@beneficiaries_id", dto.beneficiary_id),
                    new SqlParameter("@total_cost", dto.total_cost),
                    new SqlParameter("@description", dto.Description),
                    new SqlParameter("@image", dto.image_identification_url)
                    ).ToListAsync();

                PaymentRequest? request = result.FirstOrDefault();
                return request;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<PaymentRequest?> UpdatePaymentRequest(int id, double payment, string status)
        {
            try
            {
                string sql = "exec UpdatePaymentRequest " +
                    "@totalpayment, " +
                    "@status, " +
                    "@id";

                IEnumerable<PaymentRequest?> result = await _context.PaymentRequests.FromSqlRaw(sql,
                    new SqlParameter("@totalpayment", payment),
                    new SqlParameter("@status", status),
                    new SqlParameter("@id", id)).ToListAsync();

                PaymentRequest? request = result.FirstOrDefault();
                return request;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
