using backend.IRepositories;
using backend.Services;
using InsuranceManagement.UnitTests.Mocks;
using Moq;
using Xunit;

namespace InsuranceManagement.UnitTests.UnitTestServices
{
    public class VerificationPasswordServiceTests
    {
        private readonly Mock<IVerificationPasswordRepository> _mockVerificationPasswordRepository;

        public VerificationPasswordServiceTests()
        {
            _mockVerificationPasswordRepository = MockVerificationPasswordRepository.GetVerificationPasswordRepository();
        }

        [Fact]
        public async Task GenerateOTP_WithUserId_ReturnsOTPCode()
        {
            // Arrange
            var verificationPasswordService = new VerificationPasswordService(_mockVerificationPasswordRepository.Object);
            var userId = 1;

            // Act
            var result = await verificationPasswordService.GenerateOTP(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("123456", result);
        }

        [Fact]
        public async Task VerificationOTP_WithUserIdAndOTPCode_ReturnsNothing()
        {
            // Arrange
            var verificationPasswordService = new VerificationPasswordService(_mockVerificationPasswordRepository.Object);
            var userId = 1;
            var otp = "123456";

            // Act
            await verificationPasswordService.VerificationOTP(userId, otp);

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task VerificationOTP_WithUserIdAndOTPCode_NotValiad()
        {
            // Arrange
            var verificationPasswordService = new VerificationPasswordService(_mockVerificationPasswordRepository.Object);
            var userId = 3;
            var otp = "123456";

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => verificationPasswordService.VerificationOTP(userId, otp));

            // Assert
            Assert.Equal("User's verification OTP is not valid", exception.Message);
        }

        [Fact]
        public async Task VerificationOTP_WithUserIdAndOTPCode_OutOfDate()
        {
            // Arrange
            var verificationPasswordService = new VerificationPasswordService(_mockVerificationPasswordRepository.Object);
            var userId = 1;
            var otp = "123456";

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => verificationPasswordService.VerificationOTP(userId, otp));

            // Assert
            Assert.Equal("OTP code is expired", exception.Message);
        }

        [Fact]
        public async Task VerificationOTP_WithUserIdAndOTPCode_Icorrect()
        {
            // Arrange
            var verificationPasswordService = new VerificationPasswordService(_mockVerificationPasswordRepository.Object);
            var userId = 2;
            var otp = "1234567";

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => verificationPasswordService.VerificationOTP(userId, otp));

            // Assert
            Assert.Equal("OTP is not correct", exception.Message);
        }
    }
}