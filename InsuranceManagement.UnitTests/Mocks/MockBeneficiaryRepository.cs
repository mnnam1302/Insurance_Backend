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
    public static class MockBeneficiaryRepository
    {
        public static Mock<IBeneficiaryRepository> GetBeneficiaryRepository()
        {
            var listBeneficiaries = new List<Beneficiary>
            {
                new Beneficiary
                {
                    BeneficiaryId = 1,
                    Email = "beneficiary1@gmail.com",
                    FullName = "Beneficiary 1",
                    Phone = "123456789",
                    Sex = "Nam",
                    DateOfBirth = new DateTime(1999, 1, 1),
                    CardIdentification = "123456789",
                    ImageIdentificationUrl = "",
                    Address = "Address 1",
                    RelationshipPolicyholder = "Anh/Chị",
                    UserId = 1,
                    CreatedDate = DateTime.Now,
                },
                new Beneficiary
                {
                    BeneficiaryId = 2,
                    Email = "beneficiary2@gmail.com",
                    FullName = "Beneficiary 2",
                    Phone = "123456789",
                    Sex = "Nữ",
                    DateOfBirth = new DateTime(1999, 1, 1),
                    CardIdentification = "123456789",
                    ImageIdentificationUrl = "",
                    Address = "Address 1",
                    RelationshipPolicyholder = "Anh/Chị",
                    UserId = 1,
                    CreatedDate = DateTime.Now,
                }
            };

            var mockRepo = new Mock<IBeneficiaryRepository>();

            mockRepo.Setup(x => x.Add(It.IsAny<Beneficiary>()))
                .ReturnsAsync((Beneficiary beneficiary) =>
                {
                        listBeneficiaries.Add(beneficiary);
                        return beneficiary;
                });

            mockRepo.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync((int beneficiaryId) =>
                {
                    return listBeneficiaries.FirstOrDefault(x => x.BeneficiaryId == beneficiaryId);
                });

            mockRepo.Setup(x => x.GetAll())
                .ReturnsAsync(() =>
                {
                    return listBeneficiaries;
                });

            return mockRepo;
        }
    }
}
