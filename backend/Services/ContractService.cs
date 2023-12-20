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

        public string MakeInsuranceIdentity(int id, DateTime signing_date)
        {
            int devine_id = id % 1000;

            string contract_id = devine_id.ToString("0000");
            string formattedDate = signing_date.ToString("yyyyMMdd");

            return contract_id + formattedDate;
        }

        public (DateTime, int) SplitInsuranceIdentity(string identity)
        {
            if (identity.Length != 12)
            {
                throw new ArgumentException("Invalid insurance code, Please double check your input");
            }

            string signing_date_str = identity.Substring(0, 8);
            string format = "yyyyMMdd";

            DateTime signing_date;

            if (DateTime.TryParseExact(signing_date_str, format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                signing_date = result;
            }
            else
            {
                throw new ArgumentException("Invalid insurance code, Please double check your input");
            }

            string contract_id_str = identity.Substring(8, 4);
            int contract_id;

            // Using int.TryParse
            if (int.TryParse(contract_id_str, out contract_id))
            {

            }
            else
            {
                throw new ArgumentException("Invalid insurance code, Please double check your input");
            }

            return (signing_date, contract_id);
        }

        public async Task<Contract?> GetByInsuranceCode(string insurance_code)
        {
            int pseudo_id;
            DateTime signing_date;

            try
            {
                var result = SplitInsuranceIdentity(insurance_code);

                signing_date = result.Item1;
                pseudo_id = result.Item2;

                var contract = await _contract.GetByInsuranceCode(pseudo_id, signing_date);

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

                Beneficiary? beneficiary = await _beneficiaryRepository.GetBeneficiaryById(contract.beneficial_id);

                if (beneficiary == null)
                {
                    throw new ArgumentException("Beneficiary is not valid");
                }

                decimal basic_fee = registration.BasicInsuranceFee;

                decimal discount = registration.Discount;

                contract.initial_fee_per_turn = basic_fee;
                contract.discount = discount;
                contract.total_fee = basic_fee * (1 - discount);
                contract.periodic_fee = contract.total_fee / contract.total_turn;
                contract.insurance_id = registration.InsuranceId;

                Contract? result = await _contract.AddNewContract(contract);

                if (result == null)
                {
                    throw new ArgumentException("Contract not created!");
                }

                var dto = new ContractDTO
                {
                    contract_id = result.contract_id,
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
