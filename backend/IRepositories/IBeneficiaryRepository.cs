using backend.DTO.Beneficiary;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.IRepositories
{
    public interface IBeneficiaryRepository : IGenericRepository<Beneficiary>
    {
        //Task<Beneficiary?> GetBeneficiaryById(int beneficiaryId);
        //Task<Beneficiary?> CreateBeneficiary(BeneficiaryDTO beneficiaryDTO);
    }
}
