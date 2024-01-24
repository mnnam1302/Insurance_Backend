using AutoMapper;
using backend.DTO.Registration;
using backend.IRepositories;
using backend.Profiles;
using backend.Services;
using FluentAssertions;
using InsuranceManagement.UnitTests.Mocks;
using Moq;
using Xunit;

namespace InsuranceManagement.UnitTests.UnitTestServices
{
    public class RegistrationServiceTests
    {
        private readonly Mock<IRegistrationRepository> _mockRegistrationRepository;
        private readonly Mock<IBeneficiaryRepository> _mockBeneficiaryRepository;
        private readonly Mock<IInsuranceRepository> _mockInsuranceRepository;

        private readonly IMapper _mapper;

        public RegistrationServiceTests()
        {
            _mockRegistrationRepository = MockRegistrationRepository.GetRegistrationRepository();
            _mockBeneficiaryRepository = MockBeneficiaryRepository.GetBeneficiaryRepository();
            _mockInsuranceRepository = MockInsuranceRepository.GetInsuaranceRepository();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task ChangeStatusRegistration_Success()
        {
            // Arrange
            var registrationService = new RegistrationService(_mockRegistrationRepository.Object,
               _mockBeneficiaryRepository.Object,
               _mockInsuranceRepository.Object,
                _mapper);

            // Act
            var result = await registrationService.ChangeStatusRegistration(1, new UpdateStatusRegistrationDTO { Status = "Đã lập hợp đồng" });

            // Assert
            result.Should().NotBeNull();
            result.RegistrationId.Should().Be(1);
            result.RegistrationStatus.Should().Be("Đã lập hợp đồng");
        }

        [Fact]
        public async Task GetRegistrationById_Registration()
        {
            // Arrange
            var registrationService = new RegistrationService(_mockRegistrationRepository.Object,
                _mockBeneficiaryRepository.Object,
                _mockInsuranceRepository.Object,
                _mapper);

            // Act
            var result = await registrationService.GetRegistrationById(1);

            // Assert
            result.Should().NotBeNull();
            result.RegistrationId.Should().Be(1);
            result.RegistrationStatus.Should().Be("Chờ xử lý");
        }

        [Fact]
        public async Task ChangeStatusRegistration_Fail_RegistrationNotExists()
        {
            // Arrange
            var registrationService = new RegistrationService(_mockRegistrationRepository.Object,
               _mockBeneficiaryRepository.Object,
               _mockInsuranceRepository.Object,
               _mapper);

            var status = new UpdateStatusRegistrationDTO { Status = "Đã lập hợp đồng" };

            // Act
            var result = await registrationService.ChangeStatusRegistration(3, status);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateRegistrationInsurance_Success()
        {
            // Arrange
            var registrationService = new RegistrationService(
                _mockRegistrationRepository.Object,
                _mockBeneficiaryRepository.Object,
                _mockInsuranceRepository.Object,
                _mapper);

            // Act
            var result = await registrationService.CreateRegistrationInsurance(new CreateRegistrationDTO
            {
                StartDate = new System.DateTime(2024, 1, 1),
                EndDate = new System.DateTime(2025, 1, 1),
                BasicInsuranceFee = 1065000,
                Discount = 0,
                TotalSupplementalBenefitFee = 0,
                BeneficiaryId = 1,
                InsuranceId = 1
            });

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Creation successful");

            var registrations = await _mockRegistrationRepository.Object.GetAll();

            registrations.Should().NotBeNull();
            registrations.Should().HaveCount(3);
        }

        [Fact]
        public async Task CreateRegistrationInsurance_Fail_NotSuitableAge()
        {
            // Arrange
            var registrationService = new RegistrationService(
                _mockRegistrationRepository.Object,
                _mockBeneficiaryRepository.Object,
                _mockInsuranceRepository.Object,
                _mapper);

            // Act
            var result = await registrationService.CreateRegistrationInsurance(new CreateRegistrationDTO
            {
                StartDate = new System.DateTime(2024, 1, 1),
                EndDate = new System.DateTime(2025, 1, 1),
                BasicInsuranceFee = 1065000,
                Discount = 0,
                TotalSupplementalBenefitFee = 0,
                BeneficiaryId = 1,
                InsuranceId = 2
            });

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Creation failed");
            result.Errors.Should().HaveCount(1);
            result.Errors[0].Should().Be("Beneficiary's age is not suit with insurance");

            var registrations = await _mockRegistrationRepository.Object.GetAll();

            registrations.Should().NotBeNull();
            registrations.Should().HaveCount(2);
        }

        [Fact]
        public async Task CreateRegistrationInsurance_Fail_BeneficiaryNotExists()
        {
            // Arrange
            var registrationService = new RegistrationService(
                _mockRegistrationRepository.Object,
                _mockBeneficiaryRepository.Object,
                _mockInsuranceRepository.Object,
                _mapper);

            // Act
            var result = await registrationService.CreateRegistrationInsurance(new CreateRegistrationDTO
            {
                StartDate = new System.DateTime(2024, 1, 1),
                EndDate = new System.DateTime(2025, 1, 1),
                BasicInsuranceFee = 1065000,
                Discount = 0,
                TotalSupplementalBenefitFee = 0,
                BeneficiaryId = 3,
                InsuranceId = 1
            });

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Creation failed");
            result.Errors.Should().HaveCount(1);
            result.Errors[0].Should().Be("Beneficiary is not valid.");

            var registrations = await _mockRegistrationRepository.Object.GetAll();

            registrations.Should().NotBeNull();
            registrations.Should().HaveCount(2);
        }

        [Fact]
        public async Task CreateRegistrationInsurance_Fail_InsuranceNotExists()
        {
            // Arrange
            var registrationService = new RegistrationService(
                _mockRegistrationRepository.Object,
                _mockBeneficiaryRepository.Object,
                _mockInsuranceRepository.Object,
                _mapper);

            // Act
            var result = await registrationService.CreateRegistrationInsurance(new CreateRegistrationDTO
            {
                StartDate = new System.DateTime(2024, 1, 1),
                EndDate = new System.DateTime(2025, 1, 1),
                BasicInsuranceFee = 1065000,
                Discount = 0,
                TotalSupplementalBenefitFee = 0,
                BeneficiaryId = 1,
                InsuranceId = 3
            });

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Creation failed");
            result.Errors.Should().HaveCount(1);
            result.Errors[0].Should().Be("Insurance is not valid.");

            var registrations = await _mockRegistrationRepository.Object.GetAll();

            registrations.Should().NotBeNull();
            registrations.Should().HaveCount(2);
        }
    }
}