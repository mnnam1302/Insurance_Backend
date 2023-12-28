using backend.DTO;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Server;
using System.Runtime.CompilerServices;

namespace backend.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contract;
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IBeneficiaryRepository _beneficiaryRepository;

        public ContractService(IContractRepository contract, IRegistrationRepository registrationRepository, IBeneficiaryRepository beneficiaryRepository)
        {
            _contract = contract;
            _registrationRepository = registrationRepository;
            _beneficiaryRepository = beneficiaryRepository;
        }

        private int GetTotalTurn(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date must be earlier than end date");
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

        

        public async Task<List<Contract>> GetAll()
        {
            return await _contract.GetAll();
        }

        public async Task<Contract?> GetByInsuranceCode(string insurance_code)
        {

            try
            {
                var contract = await _contract.GetByInsuranceCode(insurance_code);

                if (contract == null)
                {
                    throw new ArgumentException("Invalid insurance code, Please double check your input");
                }

                return contract;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Contract?> GetById(int contract_id)
        {
            return await _contract.GetById(contract_id);
        }

        public async Task<List<Contract>> GetByUserId(int userId)
        {
            return (List<Contract>)await _contract.GetByUserId(userId);
        }


        public async Task<ContractDTO?> AddNewContract(ContractDTO contract)
        {
            try
            {
                Registration? registration = await _registrationRepository.GetById(contract.registration_id);

                if (registration == null)
                {
                    throw new ArgumentException("Registration is not valid");
                }

                decimal basic_fee = registration.BasicInsuranceFee;
                decimal discount = registration.Discount;

                DateTime start_date = registration.StartDate;
                DateTime end_date = registration.EndDate;

                contract.initial_fee_per_turn = basic_fee;
                contract.discount = discount;
                contract.total_fee = basic_fee * (1 - discount);
                contract.total_turn = GetTotalTurn(start_date, end_date);
                contract.start_date = start_date;
                contract.end_date = end_date;
                contract.periodic_fee = contract.total_fee / contract.total_turn;
                contract.beneficial_id = registration.BeneficiaryId;
                contract.insurance_id = registration.InsuranceId;

                Contract? result = await _contract.AddNewContract(contract);

                if (result == null)
                {
                    throw new ArgumentException("Contract not created!");
                }

                var dto = new ContractDTO
                {
                    contract_id = result.contract_id,
                    insurance_code = result.insurance_code,
                    signing_date = result.signing_date,
                    start_date = result.start_date,
                    end_date = result.end_date,
                    contract_status = result.contract_status,
                    initial_fee_per_turn = result.initial_fee_per_turn,
                    discount = result.discount,
                    total_fee = result.total_fee,
                    total_turn = result.total_turn,
                    periodic_fee = result.periodic_fee,
                    bonus_fee = result.total_fee,
                    beneficial_id = result.beneficial_id,
                    insurance_id = result.insurance_id,
                    user_id = result.user_id,
                    registration_id = result.registration_id,
                };

                return dto;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
