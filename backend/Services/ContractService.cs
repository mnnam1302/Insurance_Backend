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
        private string MakeInsuranceCode(int id, DateTime signing_date)
        {
            int devine_id = id % 1000;

            string contract_id = devine_id.ToString("0000");
            string formattedDate = signing_date.ToString("yyyyMMdd");

            return formattedDate + "-KHN" + contract_id;
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


        public async Task<ContractDTO?> CreateContract(int registrationId, int userId)
        {
            try
            {

                var registration = await _registrationRepository.Get(registrationId);

                if (registration == null)
                {
                    throw new Exception("Registration not exists");
                }

                var contract = new Contract();

                decimal basic_fee = registration.BasicInsuranceFee;
                decimal discount = registration.Discount;
                DateTime start_date = registration.StartDate;
                DateTime end_date = registration.EndDate;
                DateTime signingDate = DateTime.Now;

                contract.RegistrationId = registrationId;
                contract.UserId = userId;
                contract.InitialFeePerTurn = basic_fee;
                contract.Discount = discount;
                contract.TotalFee = basic_fee * (1 - discount);
                contract.TotalTurn = GetTotalTurn(start_date, end_date);
                contract.ContractStatus = "Chưa thanh toán";
                contract.SigningDate = signingDate;
                contract.StartDate = start_date;
                contract.EndDate = end_date;
                contract.PeriodFee = contract.TotalFee / contract.TotalTurn;
                contract.BeneficiaryId = registration.BeneficiaryId;

                // Tạo contract
                var result = await _contractRepository.Add(contract);

                if(result == null)
                {
                    throw new Exception("Cannot create contract!");
                }

                // Thêm Insurance Code
                string insuranceCode = MakeInsuranceCode(result.ContractId, signingDate);
                result = await _contractRepository.AddContractInsuranceCode(result, insuranceCode);

                // Cập nhật trạng thái đơn đăng ký
                string status = "Đã lập hợp đồng";
                var updateStatusRegistration = await _registrationRepository.UpdateRegistrationStatus(registration, status);

                var responseContract = _mapper.Map<ContractDTO>(result);
                return responseContract;
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
