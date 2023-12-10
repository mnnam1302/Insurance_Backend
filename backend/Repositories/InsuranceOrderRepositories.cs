using backend.DTO;
using backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class InsuranceOrderRepositories: IInsuranceOrderReponsitories
    {
        private readonly InsuranceDbContext _context;

        public InsuranceOrderRepositories (InsuranceDbContext context)
        {
            _context = context;
        }
        public async Task<List<InsuranceOrder>> GetAll()
        {
            string sql = "select * from InsuranceOrder";
            IEnumerable<InsuranceOrder> result = await _context.InsuranceOrders.FromSqlRaw(sql).ToListAsync();

            return (List<InsuranceOrder>) result;
        }

        public async Task<InsuranceOrder?> GetById(int id)
        {
            var order = await _context.InsuranceOrders.FindAsync(id);
            return order;
        }

        public async Task<InsuranceOrder?> AddInsuranceOrder(InsuranceOrderDTO dto)
        {
            try
            {
                string sql = "insert into InsuranceOrder values (ContracId, TotalCost, Descriptions) values (" +
                    "@contract_id, " +
                    "@total_cost, " +
                    "@Descriptions )";

                IEnumerable<InsuranceOrder?> result = await _context.InsuranceOrders.FromSqlRaw(sql,
                    new SqlParameter("@contract_id", dto.ContractId),
                    new SqlParameter("@total_cost", dto.TotalCost),
                    new SqlParameter("@Descriptions", dto.Description)
                    ).ToListAsync();

                InsuranceOrder? order = result.FirstOrDefault();
                return order;
            }
            catch (ArgumentException ex) 
            {
                throw new ArgumentException(ex.Message);
            }

        }
    }
}
