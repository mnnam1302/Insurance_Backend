using backend.DTO.Insurance;
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
    public static class MockInsuranceRepository
    {
        public static Mock<IInsuranceRepository> GetInsuaranceRepository()
        {
            var insuranceList = new List<Insurance>
                {
                    new Insurance
                    {
                        InsuranceId = 1,
                        NameInsurance = "Insurance 1",
                        FromAge = 0,
                        ToAge = 0,
                        Price = 1065000,
                        Discount = 0,
                        Status = "Đang hoạt động",
                        InsuranceTypeId = 1,
                    },
                    new Insurance
                    {
                        InsuranceId = 2,
                        NameInsurance = "Insurance 2",
                        FromAge = 1,
                        ToAge = 3,
                        Price = 710000,
                        Discount = 0,
                        Status = "Đang hoạt động",
                        InsuranceTypeId = 1,
                    }
                };

            var mockRepo = new Mock<IInsuranceRepository>();

            mockRepo.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync((int insuranceId) =>
                {
                    return insuranceList.FirstOrDefault(x => x.InsuranceId == insuranceId);
                });

            mockRepo.Setup(x => x.GetAllAges())
                .ReturnsAsync(() =>
                {
                    var ages = insuranceList.Select(x => new AgeDTO { FromAge = x.FromAge, ToAge = x.ToAge }).Distinct().ToList();
                    return ages;
                });

            mockRepo.Setup(x => x.GetAllInsurances(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int fromAge, int toAge) =>
                {
                    var insurances = insuranceList.Where(x => x.FromAge <= fromAge && x.ToAge >= toAge).ToList();
                    return insurances;
                });

            mockRepo.Setup(x => x.GetInsurancesByAgeCustomer(It.IsAny<int>()))
                .ReturnsAsync((int age) =>
                {
                    var insurances = insuranceList.Where(x => x.FromAge <= age && x.ToAge >= age).ToList();
                    return insurances;
                });

            return mockRepo;
        }
    }
}
