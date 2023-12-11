using backend.DTO;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class InsuranceOrderService: IInsuranceOrderService
    {
        private readonly IInsuranceOrderReponsitories _service;

        public InsuranceOrderService(IInsuranceOrderReponsitories service)
        {
            _service = service;
        }

        public async Task<List<InsuranceOrder>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<InsuranceOrder?> GetById(int id)
        {
            return await _service.GetById(id);
        }
        public async Task<InsuranceOrder?> AddInsuranceOrder(InsuranceOrderDTO dto)
        {
            return await _service.AddInsuranceOrder(dto);
        }
        public async Task<InsuranceOrder?> UpdateInsuranceOrder(InsuranceOrderDTO dto, int id)
        {
            return await _service.UpdateInsuranceOrder(dto, id);
        }
    }
}
