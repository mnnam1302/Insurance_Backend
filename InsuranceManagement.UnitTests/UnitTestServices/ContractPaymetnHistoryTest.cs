using AutoMapper;
using backend.IRepositories;
using backend.Profiles;
using backend.DTO.PaymentContractHistory;
using backend.Responses;
using backend.Services;
using FluentAssertions;
using InsuranceManagement.UnitTests.Mocks;
using Moq;
using Xunit;
using backend.Models;

namespace InsuranceManagement.UnitTests.UnitTestServices
{
    public class ContractPaymetnHistoryTest
    {
        private readonly Mock<IContractPaymentHistoryRepository> _mockContractPaymentHistory;
        private readonly IMapper _mapper;

        public ContractPaymetnHistoryTest()
        {
            _mockContractPaymentHistory = MockContractPaymentHistoryRepository.GetContractPaymentHistoryRepository();


            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task UpdatePaymentContract_Success_StatusTrue()
        {
            // Arrange
            var contractPaymentService = new ContractPaymentHistoryService(_mockContractPaymentHistory.Object,
                                                                            _mapper);

            var updatePaymetDTO = new UpdatePaymentContractHistoryDTO
            {
                PaymentContractId = 1,
                TransactionCode = "2024",
                ServicePayment = "VNPAY",
                BankName = "VIB",
                Status = "00",
            };

            // Act
            var result = await contractPaymentService.UpdatePaymentContract(updatePaymetDTO);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be("Đã thanh toán");

        }

        [Fact]
        public async Task UpdatePaymentContract_Fail_StatusFalse()
        {
            // Arrange
            var contractPaymentService = new ContractPaymentHistoryService(_mockContractPaymentHistory.Object,
                                                                            _mapper);

            var updatePaymetDTO = new UpdatePaymentContractHistoryDTO
            {
                PaymentContractId = 1,
                TransactionCode = "2024",
                ServicePayment = "VNPAY",
                BankName = "VIB",
                Status = "01",
            };

            // Act
            var result = await contractPaymentService.UpdatePaymentContract(updatePaymetDTO);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be("Thất bại");

        }

        [Fact]
        public async Task UpdateStatusPaymentContract_Success()
        {
            // Arrange
            var contractPaymentService = new ContractPaymentHistoryService(_mockContractPaymentHistory.Object,
                                                                            _mapper);

            // Act
            var result = await contractPaymentService.UpdateStatusContractPayment(3, "Đã thanh toán");

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be("Đã thanh toán");

        }

        [Fact]
        public async Task CreateContractPaymentHistory_Success()
        {
            // Arrange
            var contractPaymentService = new ContractPaymentHistoryService(_mockContractPaymentHistory.Object,
                                                                            _mapper);

            var contractPayment = new CreatePaymentContractHistoryDTO
            {
                ContractId = 1,
                PaymentAmount = 10000,
            };

            // Act
            var result = await contractPaymentService.CreatePaymentContractHistory(contractPayment);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BaseCommandResponse>();
            result .Success.Should().BeTrue();
        }
    }
}
