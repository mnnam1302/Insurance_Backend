using AutoMapper;
using backend.DTO.InsuranceType;
using backend.IRepositories;
using backend.Profiles;
using backend.Services;
using FluentAssertions;
using InsuranceManagement.UnitTests.Mocks;
using Moq;
using Xunit;

namespace InsuranceManagement.UnitTests.UnitTestServices
{
    public class InsuranceTypeServiceTests
    {
        private readonly Mock<IInsuranceTypeRepository> _mockRepo;
        private readonly IMapper _mapper;

        public InsuranceTypeServiceTests()
        {
            _mockRepo = MockInsuranceTypeRepository.GetInsuranceTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetAllInsuranceTypes_Returns_ListInsuranceTypeDTO()
        {
            // Arrange
            var insuranceTypeService = new InsuranceTypeService(_mockRepo.Object, _mapper);

            // Act
            var result = await insuranceTypeService.GetAllInsuranceTypes();

            // Assert
            result.Count.Should().Be(3);
        }

        [Fact]
        public async Task GetInsuranceTypeById_Returns_InsuranceTypeDTO()
        {
            // Arrange
            var insuranceTypeService = new InsuranceTypeService(_mockRepo.Object, _mapper);

            // Act
            var result = await insuranceTypeService.GetInsuranceTypeById(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<InsuranceTypeDTO>();
            result.InsuranceTypeId.Should().Be(1);
            result.NameInsuranceType.Should().Be("Bảo hiểm nhân thọ");
        }
    }
}