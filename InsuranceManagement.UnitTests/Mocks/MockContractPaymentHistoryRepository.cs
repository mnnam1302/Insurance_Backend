using backend.IRepositories;
using backend.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManagement.UnitTests.Mocks
{
    public static class MockContractPaymentHistoryRepository
    {
        public static Mock<IContractPaymentHistoryRepository> GetContractPaymentHistoryRepository()
        {
            var listContractPayment = new List<ContractPaymentHistory>
            {
                new ContractPaymentHistory
                {
                    PaymentContractId = 1,
                    TransactionCode = "2024",
                    PaymentDate = new DateTime(2024,1,1),
                    ServicePayment = "VNPAY",
                    BankName = "VIB",
                    Status = "Đã thanh toán",
                    ContractId = 1,
                },
                new ContractPaymentHistory
                {
                    PaymentContractId = 2,
                    TransactionCode = "2014",
                    PaymentDate = new DateTime(2023,11,1),
                    ServicePayment = "VNPAY",
                    BankName = "Vietcombank",
                    Status = "Đã thanh toán",
                    ContractId = 2,
                },
                new ContractPaymentHistory
                {
                    PaymentContractId = 3,
                    TransactionCode = "2012",
                    PaymentDate = new DateTime(2023,11,1),
                    ServicePayment = "VNPAY",
                    BankName = "Agribank",
                    Status = "00",
                    ContractId = 1,
                },
            };

            var mockRepo = new Mock<IContractPaymentHistoryRepository>();

            mockRepo.Setup(x => x.Add(It.IsAny<ContractPaymentHistory>()))
                .ReturnsAsync((ContractPaymentHistory contractPayment) =>
                {
                    listContractPayment.Add(contractPayment);
                    return contractPayment;
                });

            mockRepo.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync((int contractPaymentId) =>
                {
                    return listContractPayment.FirstOrDefault(x => x.PaymentContractId == contractPaymentId);
                });

            mockRepo.Setup(x => x.Update(It.IsAny<ContractPaymentHistory>()))
                .ReturnsAsync((ContractPaymentHistory contractPayment) =>
                {
                    var contractPaymetUpdate = listContractPayment.FirstOrDefault(x => x.PaymentContractId == contractPayment.PaymentContractId);
                    contractPaymetUpdate.Status = contractPayment.Status;
                    contractPaymetUpdate.ServicePayment = contractPayment.ServicePayment;
                    contractPaymetUpdate.BankName = contractPayment.BankName;
                    contractPaymetUpdate.PaymentDate = contractPayment.PaymentDate;
                    contractPaymetUpdate.TransactionCode = contractPayment.TransactionCode;

                    return contractPaymetUpdate;
                });

            mockRepo.Setup(x => x.UpdateStatusContractPayment(It.IsAny<ContractPaymentHistory>(), It.IsAny<string>()))
                .ReturnsAsync((ContractPaymentHistory contractPayment, string status) =>
                {
                    var contractUpdate = listContractPayment.FirstOrDefault(x => x.PaymentContractId == contractPayment.PaymentContractId);
                    contractUpdate.Status = status;
                    return contractUpdate;
                });

            return mockRepo;
        }
    }
}
