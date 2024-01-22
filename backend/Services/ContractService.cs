using AutoMapper;
using Azure;
using backend.DTO.Contract;
using backend.IRepositories;
using backend.Models;
using backend.Repositories;
using backend.Responses;
using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Server;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace backend.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IMapper _mapper;

        public ContractService(IContractRepository contractRepository, 
                                IRegistrationRepository registrationRepository,
                                IMapper mapper)
        {
            _contractRepository = contractRepository;
            _registrationRepository = registrationRepository;
            _mapper = mapper;
        }

        private int GetTotalTurn(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new Exception("Start date must be earlier than end date");
            }

            int monthsDifference = 0;
            DateTime currentDate = startDate;

            while (currentDate < endDate)
            {
                currentDate = currentDate.AddMonths(1);
                monthsDifference++;
            }

            return monthsDifference;
        }

        public async Task<List<ContractDTO>> GetListContracts()
        {
            var result = await _contractRepository.GetAll();

            var response = _mapper.Map<List<ContractDTO>>(result);
            return response;
        }

        public async Task<ContractDTO> GetContractById(int contract_id)
        {
            var result = await _contractRepository.Get(contract_id);

            var response = _mapper.Map<ContractDTO>(result);
            return response;
        }

        public async Task<ContractDTO?> GetByInsuranceCode(string insurance_code)
        {
            try
            {
                var result = await _contractRepository.GetContractByInsuranceCode(insurance_code);

                var response = _mapper.Map<ContractDTO>(result);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ContractDTO>> GetByUserId(int userId)
        {
            var result = await _contractRepository.GetContractByUserId(userId);

            var response = _mapper.Map<List<ContractDTO>>(result);
            return response;
        }


        public async Task<ContractDTO> CreateContract(ContractDTO contractDTO)
        {
            try
            {
                var registration = await _registrationRepository.Get(contractDTO.RegistrationId);

                if (registration == null)
                {
                    throw new Exception("Registration is not found");
                }

                decimal basic_fee = registration.BasicInsuranceFee;
                decimal discount = registration.Discount;
                DateTime start_date = registration.StartDate;
                DateTime end_date = registration.EndDate;
                contractDTO.InitialFeePerTurn = basic_fee;
                contractDTO.Discount = discount;
                contractDTO.TotalFee = basic_fee * (1 - discount);
                contractDTO.TotalTurn = GetTotalTurn(start_date, end_date);
                contractDTO.StartDate = start_date;
                contractDTO.EndDate = end_date;
                contractDTO.PeriodFee = contractDTO.TotalFee / contractDTO.TotalTurn;
                contractDTO.BeneficiayId = registration.BeneficiaryId;

                // Tạo contract
                var result  = await _contractRepository.CreateContract(contractDTO);

                // Cập nhật trạng thái đơn đăng ký
                string status = "Đã lập hợp đồng";
                var updateStatusRegistration = await _registrationRepository.UpdateRegistrationStatus(registration, status);

                
                var response = _mapper.Map<ContractDTO>(result);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SummaryContractDTO> GetSummaryContract()
        {
            var response = new SummaryContractDTO();
            // Lấy danh thu 2 tháng gần đây nhất
            var result = await _contractRepository.GetSummaryContract();
            
            response.RevenueCurrentMonth = result[0].Revenue;

            // Tính tỷ lệ tháng này bằng bao nhiêu phần trăm tháng trước
           if (result.Count >= 2)
           {
                var rating = (result[0].Revenue - result[1].Revenue) / (result[0].Revenue + result[1].Revenue) * 100;

                response.RateRevenueCurrentMonth = Math.Round(rating, 2);
           }

            return response;

        }
        public async Task<ContractDTO> UpdateStatusContract(int contractId, string status)
        {
            var contract = await _contractRepository.Get(contractId);

            var result = await _contractRepository.UpdateContractStatus(contract, status);

            var response = _mapper.Map<ContractDTO>(result);
            return response;
        }
    }
}
