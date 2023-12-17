using backend.DTO;
using backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static backend.Repositories.PaymentRequestRepository;

namespace backend.Repositories
{
    public class PaymentRequestRepository
    {
        public class PaymentRequestReponsitory : IPaymentRequestRepository
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
                        new SqlParameter("@beneficiaries_id", dto.contract_id),
                        new SqlParameter("@total_cost", dto.total_cost),
                        new SqlParameter("@description", dto.Description),
                        new SqlParameter("@image", dto.image_identification_url ?? "")
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
                var paymentDomain = _context.PaymentRequests.FirstOrDefault(x => x.paymentrequest_id == id);

                if (paymentDomain == null)
                {
                    return null;
                }

                paymentDomain.total_payment = payment;
                paymentDomain.Status = status;

                await _context.SaveChangesAsync();

                var request = new PaymentRequest
                {
                    paymentrequest_id = paymentDomain.paymentrequest_id,
                    total_cost = paymentDomain.total_cost,
                    total_payment = paymentDomain.total_payment,
                    Description = paymentDomain.Description,
                    image_identification_url = paymentDomain.image_identification_url,
                    Status = paymentDomain.Status,
                    contract_id = paymentDomain.contract_id,
                    update_date = paymentDomain.update_date,
                };

                return request;
            }
        }
    }
}
