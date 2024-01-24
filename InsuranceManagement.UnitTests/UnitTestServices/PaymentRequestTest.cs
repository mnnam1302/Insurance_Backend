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

namespace InsuranceManagement.UnitTests.UnitTestServices
{
    public class PaymentRequestTest
    {
        private readonly Mock<IPaymentRequestRepository> _mockPaymentRequestRepository;

        private readonly IMapper _mapper;

        public PaymentRequestTest()
        {
            _mockPaymentRequestRepository = MockPaymentRequest.GetPaymentRequestRepository();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task UpdatePaymentRequest_Success()
        {
            // Arrange
            var paymentRequestService = new PaymentRequestService(_mockPaymentRequestRepository.Object, _mapper);
            
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
            var paymentRequestService = new PaymentRequestService(_mockPaymentRequestRepository.Object, _mapper);

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
    }
}
