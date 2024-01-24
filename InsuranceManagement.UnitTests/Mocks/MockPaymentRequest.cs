using backend.IRepositories;
using backend.Models;
using backend.DTO.Contract;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Identity.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.DTO.PaymentRequest;

namespace InsuranceManagement.UnitTests.Mocks
{
    public class MockPaymentRequest
    {
        public static Mock<IPaymentRequestRepository> GetPaymentRequestRepository()
        {
            var listPaymentRequests = new List<PaymentRequest>
            {
                new PaymentRequest
                {
                    PaymentRequestId = 1,
                    TotalCost = 123456789,
                    TotalPayment = 0,
                    Description = "Unknow",
                    ImagePaymentRequestUrl = string.Empty,
                    RequestStatus = "Chờ xử lý",
                    ContractId = 1,
                    UpdatedDate = new DateTime(2024, 1, 1),
                },
                new PaymentRequest
                {
                    PaymentRequestId = 2,
                    TotalCost = 123456789,
                    TotalPayment = 123456,
                    Description = "No Name",
                    ImagePaymentRequestUrl = string.Empty,
                    RequestStatus = "Đã xử lý",
                    ContractId = 1,
                    UpdatedDate = new DateTime(2024, 1, 1),
                },
            };

            var mockRepo = new Mock<IPaymentRequestRepository>();

            mockRepo.Setup(m => m.GetAll())
                .ReturnsAsync(listPaymentRequests);

            mockRepo.Setup(m => m.Get(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    return listPaymentRequests.FirstOrDefault(x => x.PaymentRequestId == id);

                });

            mockRepo.Setup(x => x.UpdatePaymentRequest(It.IsAny<PaymentRequest>(), It.IsAny<UpdatePaymentRequestDTO>()))
                .ReturnsAsync((PaymentRequest paymentRequest, UpdatePaymentRequestDTO updateDTO) =>
                {
                    var paymentRequestUpdate = listPaymentRequests.FirstOrDefault(x => x.PaymentRequestId == paymentRequest.PaymentRequestId);
                    paymentRequestUpdate.TotalPayment = updateDTO.Payment;
                    paymentRequestUpdate.RequestStatus = updateDTO.Status;

                    return paymentRequestUpdate;
                });

            mockRepo.Setup(x => x.Add(It.IsAny<PaymentRequest>()))
               .ReturnsAsync((PaymentRequest paymentRequest) =>
               {
                   listPaymentRequests.Add(paymentRequest);
                   return paymentRequest;
               });

            return mockRepo;
        }
    }
}
