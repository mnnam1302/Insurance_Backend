using AutoMapper;
using backend.DTO.InsuranceType;
using backend.IRepositories;
using backend.Models;

namespace backend.Services
{
    public class InsuranceTypeService : IInsuranceTypeService
    {
        private readonly IInsuranceTypeRepository _insuranceRepository;
        private readonly IMapper _mapper;
        public InsuranceTypeService(IInsuranceTypeRepository insuranceRepository, IMapper mapper)
        {
            _insuranceRepository = insuranceRepository;
            _mapper = mapper;
        }

        public async Task<List<InsuranceTypeDTO>> GetAllInsuranceTypes()
        {
            var result = await _insuranceRepository.GetAll();
            return _mapper.Map<List<InsuranceTypeDTO>>(result);
        }

        public async Task<InsuranceTypeDTO?> GetInsuranceTypeById(int id)
        {
            var result = await _insuranceRepository.Get(id);
            return _mapper.Map<InsuranceTypeDTO>(result);
        }
    }
}
