using backend.IRepositories;
using backend.Models;
using Moq;

namespace InsuranceManagement.UnitTests.Mocks
{
    public static class MockInsuranceTypeRepository
    {
        public static Mock<IInsuranceTypeRepository> GetInsuranceTypeRepository()
        {
            var insuranceTypes = new List<InsuranceType>
            {
                new InsuranceType
                {
                    InsuranceTypeId = 1,
                    NameInsuranceType = "Bảo hiểm nhân thọ"
                },
                new InsuranceType
                {
                    InsuranceTypeId = 2,
                    NameInsuranceType = "Bảo hiểm ô tô"
                },
                new InsuranceType
                {
                    InsuranceTypeId = 3,
                    NameInsuranceType = "Bảo hiểm du lịch"
                },
            };

            var mockRepo = new Mock<IInsuranceTypeRepository>();

            mockRepo.Setup(x => x.GetAll()).ReturnsAsync(insuranceTypes);

            mockRepo.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync((int id) => insuranceTypes.FirstOrDefault(x => x.InsuranceTypeId == id));

            return mockRepo;
        }
    }
}