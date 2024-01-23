using AutoMapper;
using backend.DTO.Beneficiary;
using backend.IRepositories;
using backend.Profiles;
using backend.Responses;
using backend.Services;
using FluentAssertions;
using InsuranceManagement.UnitTests.Mocks;
using Moq;
using Xunit;

namespace InsuranceManagement.UnitTests.UnitTestServices
{
    public class BeneficiaryServiceTests
    {
        private readonly Mock<IBeneficiaryRepository> _mockBeneficiaryRepository;
        private readonly IMapper _mapper;

        public BeneficiaryServiceTests()
        {
            _mockBeneficiaryRepository = MockBeneficiaryRepository.GetBeneficiaryRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetBeneficiaryById_WithValidId_ReturnsBeneficiary()
        {
            // Arrange
            var beneficiaryService = new BeneficiaryService(_mockBeneficiaryRepository.Object, _mapper);

            // Act
            var result = await beneficiaryService.GetBeneficiaryById(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BeneficiaryDTO>();
            result.BeneficiaryId.Should().Be(1);
        }

        [Fact]
        public async Task GetBeneficiaryById_WithInvalidId_ReturnsNull()
        {
            // Arrange
            var beneficiaryService = new BeneficiaryService(_mockBeneficiaryRepository.Object, _mapper);

            // Act
            var result = await beneficiaryService.GetBeneficiaryById(4);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateBeneficiary_WithValidBeneficiary_ReturnsBeneficiary()
        {
            // Arrange
            var beneficiaryService = new BeneficiaryService(_mockBeneficiaryRepository.Object, _mapper);

            var beneficiary = new CreateBeneficiaryDTO
            {
                Email = "beneficiary3@gmail.com",
                FullName = "Beneficiary 3",
                Phone = "123456789",
                Sex = "Nam",
                DateOfBirth = new DateTime(2000, 1, 1),
                CardIdentification = "123456789",
                ImageIdentificationUrl = "",
                Address = "Address 1",
                RelationshipPolicyholder = "Anh/Chị",
                UserId = 1,
            };

            // Act
            var result = await beneficiaryService.CreateBeneficiary(beneficiary);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BaseCommandResponse>();
            result.Success.Should().BeTrue();

            var beneficiaries = await _mockBeneficiaryRepository.Object.GetAll();

            beneficiaries.Should().NotBeNull();
            beneficiaries.Count.Should().Be(3);
        }
    }
}