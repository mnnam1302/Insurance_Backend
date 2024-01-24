using AutoMapper;
using backend.Models;
using backend.DTO.PaymentRequest;
using backend.IRepositories;
using backend.Profiles;
using backend.Services;
using FluentAssertions;
using InsuranceManagement.UnitTests.Mocks;
using Moq;
using Xunit;
using backend.Responses;

namespace InsuranceManagement.UnitTests.UnitTestServices
{
    public class PaymentRequestTest
    {
        private readonly Mock<IPaymentRequestRepository> _mockPaymentRequestRepository;
        private readonly Mock<IContractRepository> _mockContractRepository;

        private readonly IMapper _mapper;

        public PaymentRequestTest()
        {
            _mockPaymentRequestRepository = MockPaymentRequest.GetPaymentRequestRepository();
            _mockContractRepository = MockContractRepository.GetContractReopository();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetAllPaymentRequest_Success()
        {
            // Arrange
            var contractService = new PaymentRequestService(_mockPaymentRequestRepository.Object,
               _mockContractRepository.Object,
                _mapper);

            // Act
            var result = await contractService.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetPaymnetRequestById_Success()
        {
            // Arrange
            var contractService = new PaymentRequestService(_mockPaymentRequestRepository.Object,
               _mockContractRepository.Object,
                _mapper);

            // Act
            var result = await contractService.GetById(1);

            // Assert
            result.Should().NotBeNull();
            result.PaymentRequestId.Should().Be(1);
        }

        [Fact]
        public async Task UpdatePaymentRequest_Success()
        {
            // Arrange
            var paymentRequestService = new PaymentRequestService(_mockPaymentRequestRepository.Object,
                _mockContractRepository.Object,
                _mapper);
            
            var paymentUpdate = new UpdatePaymentRequestDTO
            {
                Payment = 2000,
                Status = "Đã thanh toán"
            };

            // Act
            var result = await paymentRequestService.UpdatePaymentRequest(1, paymentUpdate);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdatePaymentRequest_Fail_PaymentRequestNotFound()
        {
            // Arrange
            var paymentRequestService = new PaymentRequestService(_mockPaymentRequestRepository.Object,
                _mockContractRepository.Object,
                _mapper);

            var paymentUpdate = new UpdatePaymentRequestDTO
            {
                Payment = 2000,
                Status = "Đã thanh toán"
            };

            // Act
            var result = await paymentRequestService.UpdatePaymentRequest(3, paymentUpdate);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreatePaymentRequest_Success()
        {
            // Arrange
            var paymentRequestService = new PaymentRequestService(_mockPaymentRequestRepository.Object,
                _mockContractRepository.Object,
                _mapper);

            var paymentRequestDTO = new CreatePaymentRequestDTO
            {
                ContractId = 1,
                TotalCost = 2000,
                Description = "Test",
                ImagePaymentRequest = null,
                ImagePaymentRequestUrl = string.Empty,
                RequestStatus = "Chưa xử lý",
                UpdateDate = DateTime.Now,
            };

            // Act
            var result = await paymentRequestService.CreatePaymentRequest(paymentRequestDTO);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BaseCommandResponse>();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task CreatePaymentRequest_Fail_ContractNotValid()
        {
            // Arrange
            var paymentRequestService = new PaymentRequestService(_mockPaymentRequestRepository.Object,
                _mockContractRepository.Object,
                _mapper);

            var paymentRequestDTO = new CreatePaymentRequestDTO
            {
                ContractId = 20,
                TotalCost = 2000,
                Description = "Test",
                ImagePaymentRequest = null,
                ImagePaymentRequestUrl = string.Empty,
                RequestStatus = "Chưa xử lý",
                UpdateDate = DateTime.Now,
            };

            // Act
            var result = await paymentRequestService.CreatePaymentRequest(paymentRequestDTO);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BaseCommandResponse>();
            result.Success.Should().BeFalse();
        }
    }
}
