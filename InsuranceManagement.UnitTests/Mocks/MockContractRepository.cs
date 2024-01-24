using backend.IRepositories;
using backend.Models;
using backend.Models.Views;
using backend.DTO.Contract;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Identity.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManagement.UnitTests.Mocks
{
    public static class MockContractRepository
    {
        public static Mock<IContractRepository> GetContractReopository()
        {
            var listContract = new List<Contract>
            {
                new Contract()
                {
                    ContractId = 1,
                    RegistrationId = 1,
                    InsuranceCode = "202401010001",
                    SigningDate =  new DateTime(2024,1,1),
                    StartDate = new DateTime(2024,1,1),
                    EndDate = new DateTime(2025,1,1),
                    TotalTurn = 2,
                    ContractStatus = "Chờ xử lý",
                    InitialFeePerTurn = 1065000,
                    PeriodFee = 1065000 / 2,
                    UserId = 1,
                    BeneficiaryId = 1,
                    BonusFee = 0,
                    Discount = 0,
                },
                 new Contract()
                {
                    ContractId = 2,
                    RegistrationId = 2,
                    InsuranceCode = "202401010002",
                    SigningDate =  new DateTime(2023,10,1),
                    StartDate = new DateTime(2023,10,1),
                    EndDate = new DateTime(2025,1,1),
                    TotalTurn = 3,
                    ContractStatus = "Chờ xử lý",
                    InitialFeePerTurn = 710000,
                    PeriodFee = 710000 / 3,
                    UserId = 2,
                    BeneficiaryId = 1,
                    BonusFee = 0,
                    Discount = 0,
                },
            };

            var mockRepo = new Mock<IContractRepository>();

            mockRepo.Setup(m => m.GetAll())
                .ReturnsAsync(listContract);

            mockRepo.Setup(m => m.Get(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    return listContract.FirstOrDefault(x => x.ContractId == id);

                });

            mockRepo.Setup(x => x.UpdateContractStatus(It.IsAny<Contract>(), It.IsAny<string>()))
                .ReturnsAsync((Contract contract, string status) =>
                {
                    var contractUpdate = listContract.FirstOrDefault(x => x.ContractId == contract.ContractId);
                    contractUpdate.ContractStatus = status;
                    return contractUpdate;
                });

            mockRepo.Setup(x => x.GetContractByUserId(It.IsAny<int>()))
                .ReturnsAsync((int userId) =>
                {
                    var matchingContracts = listContract.Where(x => x.UserId == userId).ToList();
                    return matchingContracts;
                });

            mockRepo.Setup(x => x.GetContractByInsuranceCode(It.IsAny<string>()))
                .ReturnsAsync((string insuranceCode) =>
                {
                    return listContract.FirstOrDefault(x => x.InsuranceCode == insuranceCode);
                });

            mockRepo.Setup(x => x.Add(It.IsAny<Contract>()))
                .ReturnsAsync((Contract contract) =>
                {
                    listContract.Add(contract);
                    return contract;
                });

            return mockRepo;
        }
    }
}
