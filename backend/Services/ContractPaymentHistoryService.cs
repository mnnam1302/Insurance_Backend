using AutoMapper;
using backend.DTO.PaymentContractHistory;
using backend.IRepositories;
using backend.Models;
using backend.Models.Views;
using backend.Responses;

namespace backend.Services
{
    public class ContractPaymentHistoryService : IContractPaymentHistoryService
    {
        private readonly IContractPaymentHistoryRepository _contractPaymentHistoryRepository;
        private readonly IMapper _mapper;

        public ContractPaymentHistoryService(IContractPaymentHistoryRepository contractPaymentHistoryRepository,
                                                IMapper mapper)
        {
            _contractPaymentHistoryRepository = contractPaymentHistoryRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> CreatePaymentContractHistory(CreatePaymentContractHistoryDTO paymentContractDTO)
        {
            var response = new BaseCommandResponse();
            var paymentContract = _mapper.Map<ContractPaymentHistory>(paymentContractDTO);

            var result = await _contractPaymentHistoryRepository.Add(paymentContract);

            if (result == null)
            {
                response.Success = false;
                response.Message = "Create payment contract history failed.";
                response.Errors = new List<string> { "Something was wrong" };
            } else
            {
                response.Success = true;
                response.Message = "Create payment contract history successfully.";
                response.Id = result.PaymentContractId;
            }

            return response;
        }

        public async Task<PaymentContractHistoryDTO> UpdatePaymentContract(UpdatePaymentContractHistoryDTO paymentContractDTO)
        {
            var paymentContractHistory = await _contractPaymentHistoryRepository.Get(paymentContractDTO.PaymentContractId);

            _mapper.Map(paymentContractDTO, paymentContractHistory);

            if(paymentContractDTO.Status == "00")
            {
                paymentContractHistory.Status = "Đã thanh toán";
            } else
            {
                paymentContractHistory.Status = "Thất bại";
            }

            var result = await _contractPaymentHistoryRepository.Update(paymentContractHistory);

            var response = _mapper.Map<PaymentContractHistoryDTO>(result);
            return response;
        }

        public async Task<PaymentContractHistoryDTO> UpdateStatusContractPayment(int paymentContractId, string status)
        {
            var contractPayment = await _contractPaymentHistoryRepository.Get(paymentContractId);

            var result = await _contractPaymentHistoryRepository.UpdateStatusContractPayment(contractPayment, status);

            var response = _mapper.Map<PaymentContractHistoryDTO>(result);
            return response;
        }

        public async Task<List<SummaryPaymentContractDTO>> GetSummaryPaymentContract(int year)
        {
            var result = await _contractPaymentHistoryRepository.GetSummaryPaymentContract(year);

            var response = _mapper.Map<List<SummaryPaymentContractDTO>>(result);
            return response;
        }
    }
}
