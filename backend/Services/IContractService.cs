using backend.Models;
using backend.Services;
using backend.DTO.Contract;
using backend.Responses;

namespace backend.Services
{
    public interface IContractService
    {
        Task<List<ContractDTO>> GetListContracts();
        Task<ContractDTO> GetContractById(int contract_id);
        Task<List<ContractDTO>> GetByUserId(int userId);
        Task<ContractDTO?> GetByInsuranceCode(string insurance_code);
        Task<BaseCommandResponse> CreateContract(ContractDTO contract);
    }

}