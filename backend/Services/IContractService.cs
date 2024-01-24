using backend.Models;
using backend.Services;
using backend.DTO.Contract;
using backend.Responses;
using Microsoft.OpenApi.Models;

namespace backend.Services
{
    public interface IContractService
    {
        Task<List<ContractDTO>> GetListContracts();
        Task<ContractDTO> GetContractById(int contract_id);
        Task<List<ContractDTO>> GetByUserId(int userId);
        Task<ContractDTO?> GetByInsuranceCode(string insurance_code);
        Task<ContractDTO?> CreateContract(int registrationId, int userId);
        Task<ContractDTO> UpdateStatusContract(int contractId, string status);
        Task<SummaryContractDTO> GetSummaryContract();
    }

}