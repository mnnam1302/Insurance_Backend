using AutoMapper;
using backend.IRepositories;
using backend.Profiles;
using backend.Services;
using FluentAssertions;
using InsuranceManagement.UnitTests.Mocks;
using Moq;
using Xunit;

namespace InsuranceManagement.UnitTests.UnitTestServices
{
    public class InsuranceServiceTests
    {
        private readonly Mock<IInsuranceRepository> _mockInsuranceRepository;
        private readonly IMapper _mapper;

        public InsuranceServiceTests()
        {
            _mockInsuranceRepository = MockInsuranceRepository.GetInsuaranceRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetAllAges_ReturnsAges()
        {
            // Arrange
            var insuranceService = new InsuranceService(_mockInsuranceRepository.Object, null);

            // Act
            var result = await insuranceService.GetAllAges();

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
            result[0].Should().BeEquivalentTo(new { FromAge = 0, ToAge = 0 });
            result[1].Should().BeEquivalentTo(new { FromAge = 1, ToAge = 3 });
        }

        [Fact]
        public async Task GetInsurancesByAgeCustomer_ReturnsInsurances()
        {
            // Arrange
            var insuranceService = new InsuranceService(_mockInsuranceRepository.Object, _mapper);

            // Act
            var result = await insuranceService.GetInsurancesByAgeCustomer(2);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result[0].Should().BeEquivalentTo(new
            {
                InsuranceId = 2,
                NameInsurance = "Insurance 2",
                FromAge = 1,
                ToAge = 3,
                Price = 710000,
                Discount = 0,
                Status = "Đang hoạt động",
                InsuranceTypeId = 1,
            });
        }

        [Fact]
        public async Task GetAllInsurances_ReturnsInsurances()
        {
            // Arrange
            var insuranceService = new InsuranceService(_mockInsuranceRepository.Object, _mapper);

            // Act
            var result = await insuranceService.GetAllInsurances(0, 0);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result[0].Should().BeEquivalentTo(new
            {
                InsuranceId = 1,
                NameInsurance = "Insurance 1",
                FromAge = 0,
                ToAge = 0,
                Price = 1065000,
                Discount = 0,
                Status = "Đang hoạt động",
                InsuranceTypeId = 1,
            });
        }

        [Fact]
        public async Task GetInsuranceById_ReturnsInsurance()
        {
            // Arrange
            var insuranceService = new InsuranceService(_mockInsuranceRepository.Object, _mapper);

            // Act
            var result = await insuranceService.GetInsuranceById(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new
            {
                InsuranceId = 1,
                NameInsurance = "Insurance 1",
                FromAge = 0,
                ToAge = 0,
                Price = 1065000,
                Discount = 0,
                Status = "Đang hoạt động",
                InsuranceTypeId = 1,
            });
        }
    }
}