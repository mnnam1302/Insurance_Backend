using backend.DTO;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository)
        {
            _beneficiaryRepository = beneficiaryRepository;
        }
        public async Task<Beneficiary?> CreateBeneficiary(BeneficiaryDTO beneficiaryDTO)
        {
            return await _beneficiaryRepository.CreateBeneficiary(beneficiaryDTO);
        }

        public async Task<Beneficiary?> GetBeneficiaryById(int beneficiaryId)
        {
            return await _beneficiaryRepository.GetBeneficiaryById(beneficiaryId);
        }
    }
}
