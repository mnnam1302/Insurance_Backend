using backend.Models;
using backend.DTO;
using backend.Services;

namespace backend.Services
{
    public interface IContractService
    {
        //string MakeInsuranceIdentity(int id, DateTime signing_date);
        (DateTime, int) SplitInsuranceIdentity(string identity);
        Task<List<Contract>> GetAll();
        Task<List<Contract>> GetByUserId(int userId);
        Task<Contract?> GetByInsuranceCode(string insurance_code);
        Task<Contract?> GetById(int contract_id);
        Task<ContractDTO?> AddNewContract(ContractDTO contract);
    }

}