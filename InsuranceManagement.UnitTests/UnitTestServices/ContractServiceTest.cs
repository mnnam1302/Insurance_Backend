using AutoMapper;
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
    public class ContractServiceTest
    {
        private readonly Mock<IRegistrationRepository> _mockRegistrationRepository;
        private readonly Mock<IContractRepository> _mockContractRepository;

        private readonly IMapper _mapper;

        public ContractServiceTest()
        {
            _mockRegistrationRepository = MockRegistrationRepository.GetRegistrationRepository();
            _mockContractRepository = MockContractRepository.GetContractReopository();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetAllContract_Success()
        {
            // Arrange
            var contractService = new ContractService(_mockContractRepository.Object,
               _mockRegistrationRepository.Object,
                _mapper);

            // Act
            var result = await contractService.GetListContracts();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task ChangeStatusContract_Success()
        {
            // Arrange
            var contractService = new ContractService(_mockContractRepository.Object,
               _mockRegistrationRepository.Object,
                _mapper);

            // Act
            var result = await contractService.UpdateStatusContract(1, "Đã thanh toán");

            // Assert
            result.Should().NotBeNull();
            result.ContractId.Should().Be(1);
            result.ContractStatus.Should().Be("Đã thanh toán");
        }

        [Fact]
        public async Task GetContractById_Success()
        {
            // Arrange
            var contractService = new ContractService(_mockContractRepository.Object,
               _mockRegistrationRepository.Object,
                _mapper);

            // Act
            var result = await contractService.GetContractById(1);

            // Assert
            result.Should().NotBeNull();
            result.ContractId.Should().Be(1);
        }

        [Fact]
        public async Task GetContractById_Fail_ContractNotExists()
        {
            // Arrange
            var contractService = new ContractService(_mockContractRepository.Object,
               _mockRegistrationRepository.Object,
                _mapper);

            // Act
            var result = await contractService.GetContractById(3);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetContractByUserId_Success()
        {
            // Arrange
            var contractService = new ContractService(_mockContractRepository.Object,
               _mockRegistrationRepository.Object,
                _mapper);

            // Act
            var result = await contractService.GetByUserId(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetContactByUserId_Fail_NotFound()
        {
            // Arrange
            var contractService = new ContractService(_mockContractRepository.Object,
               _mockRegistrationRepository.Object,
                _mapper);

            // Act
            var result = await contractService.GetByUserId(3);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetConractByInsuranceCode_Success()
        {
            // Arrange
            var contractService = new ContractService(_mockContractRepository.Object,
               _mockRegistrationRepository.Object,
                _mapper);

            // Act
            var result = await contractService.GetByInsuranceCode("202401010001");

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetContactByInsuranceCode_Fail_NotFound()
        {
            // Arrange
            var contractService = new ContractService(_mockContractRepository.Object,
               _mockRegistrationRepository.Object,
                _mapper);

            // Act
            var result = await contractService.GetByInsuranceCode("2024010120003");

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateContract_Success()
        {
            // Arrange
            var contractService = new ContractService(_mockContractRepository.Object,
               _mockRegistrationRepository.Object,
                _mapper);

            // Act
            var result = await contractService.CreateContract(1, 1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BaseCommandResponse>();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task CreateContract_Fail_RegistrationNotValid()
        {
            // Arrange
            var contractService = new ContractService(_mockContractRepository.Object,
               _mockRegistrationRepository.Object,
                _mapper);

            // Act
            var result = await contractService.CreateContract(3, 1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BaseCommandResponse>();
            result.Success.Should().BeFalse();
        }
    }
}

