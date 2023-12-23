﻿using backend.DTO;
using backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly InsuranceDbContext _context;

        public ContractRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contract>> GetAll()
        {
            try
            {
                string sql = "select * from contracts";

                IEnumerable<Contract> contracts = await _context.Contracts.FromSqlRaw(sql).ToListAsync();

                return (List<Contract>)contracts;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Contract?> GetById(int contract_id)
        {
            try
            {
                var contract = await _context.Contracts.FindAsync(contract_id);
                return contract;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<List<Contract>> GetByUserId(int user_id)
        {
            try
            {
                string sql = "select * from contracts where user_id = @id";

                IEnumerable<Contract> result = await _context.Contracts.FromSqlRaw(sql,
                    new SqlParameter("@id", user_id)).ToListAsync();

                return (List<Contract>)result;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Contract?> GetByInsuranceCode(string insurance_code)
        {
            try
            {
                string sql = "select * from contracts" +
                    " where insurance_code = @insurance_code";

                IEnumerable<Contract> result = await _context.Contracts.FromSqlRaw(sql,
                    new SqlParameter("@insurance_code", insurance_code))
                    .ToListAsync();

                Contract? contract = result.FirstOrDefault();

                return contract;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Contract?> AddNewContract(ContractDTO dto)
        {
            try
            {
                string sql = "exec AddContract " +
                                "@start_date ," +
                                "@end_date ," +
                                "@registion_id ," +
                                "@beneficiary_id ," +
                                "@insurance_id ," +
                                "@initial_fee , " +
                                "@discount , " +
                                "@total_fee ," +
                                "@periodic_fee ," +
                                "@user_id ," +
                                "@total_turn ";

                IEnumerable<Contract> result = await _context.Contracts.FromSqlRaw(sql,
                    new SqlParameter("@start_date", dto.start_date),
                    new SqlParameter("@end_date", dto.end_date),
                    new SqlParameter("@registion_id", dto.registration_id),
                    new SqlParameter("@beneficiary_id", dto.beneficial_id),
                    new SqlParameter("@insurance_id", dto.insurance_id),
                    new SqlParameter("@initial_fee", dto.initial_fee_per_turn),
                    new SqlParameter("@discount", dto.discount),
                    new SqlParameter("@total_fee", dto.total_fee),
                    new SqlParameter("@periodic_fee", dto.periodic_fee),
                    new SqlParameter("@user_id", dto.user_id),
                    new SqlParameter("@total_turn", dto.total_turn)).ToListAsync();

                Contract? contract = result.FirstOrDefault();

                return contract;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

    }
}