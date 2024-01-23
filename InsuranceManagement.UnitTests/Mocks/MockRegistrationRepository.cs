using backend.IRepositories;
using backend.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManagement.UnitTests.Mocks
{
    public static class MockRegistrationRepository
    {
        public static Mock<IRegistrationRepository> GetRegistrationRepository()
        {
            var listRegistrations = new List<Registration>
            {
                new Registration
                {
                    RegistrationId = 1,
                    RegistrationDate = DateTime.Now,
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2025, 1, 1),
                    BasicInsuranceFee = 1065000,
                    Discount = 0,
                    TotalSupplementalBenefitFee = 0,
                    RegistrationStatus = "Chờ xử lý",
                    BeneficiaryId = 1,
                },
                new Registration
                {
                    RegistrationId = 2,
                    RegistrationDate = DateTime.Now,
                    StartDate = new DateTime(2024, 2, 1),
                    EndDate = new DateTime(2025, 2, 1),
                    BasicInsuranceFee = 710000,
                    Discount = 0,
                    TotalSupplementalBenefitFee = 0,
                    RegistrationStatus = "Chờ xử lý",
                    BeneficiaryId = 2,
                }
            };

            var mockRepo = new Mock<IRegistrationRepository>();

            mockRepo.Setup(m => m.GetAll())
                .ReturnsAsync(listRegistrations);

            mockRepo.Setup(m => m.Get(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    return listRegistrations.FirstOrDefault(x => x.RegistrationId == id);
                });

            mockRepo.Setup(x => x.UpdateRegistrationStatus(It.IsAny<Registration>(), It.IsAny<string>()))
                .ReturnsAsync((Registration registration, string status) =>
                {
                    var registrationUpdate = listRegistrations.FirstOrDefault(x => x.RegistrationId == registration.RegistrationId);
                    registrationUpdate.RegistrationStatus = status;
                    return registrationUpdate;
                });

            mockRepo.Setup(x => x.Add(It.IsAny<Registration>()))
                .ReturnsAsync((Registration registration) =>
                {
                    listRegistrations.Add(registration);
                    return registration;
                });

            return mockRepo;
        }
    }
}
