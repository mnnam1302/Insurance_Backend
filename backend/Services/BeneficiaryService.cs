using AutoMapper;
using backend.DTO.Beneficiary;
using backend.IRepositories;
using backend.Models;
using backend.Responses;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly IMapper _mapper;

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IMapper mapper)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> CreateBeneficiary(CreateBeneficiaryDTO beneficiaryDTO)
        {
            var response = new BaseCommandResponse();
            var beneficiary = _mapper.Map<Beneficiary>(beneficiaryDTO);

            var result = await _beneficiaryRepository.Add(beneficiary);
            
            if (result == null)
            {
                response.Success = false;
                response.Message = "Creation failed.";
                response.Errors = new List<string> { "Creation benificiary is failed." };
            } 
            else 
            {
                response.Success = true;
                response.Message = "Creation successful.";
                response.Id = result.BeneficiaryId;
            }
            return response;
        }

        public async Task<BeneficiaryDTO?> GetBeneficiaryById(int beneficiaryId)
        {
            var beneficiary = await _beneficiaryRepository.Get(beneficiaryId);

            var result = _mapper.Map<BeneficiaryDTO>(beneficiary);
            return result;
        }
    }
}
