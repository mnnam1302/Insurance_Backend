using backend.IRepositories;
using backend.Models;
using Moq;

namespace InsuranceManagement.UnitTests.Mocks
{
    public static class MockVerificationPasswordRepository
    {
        public static Mock<IVerificationPasswordRepository> GetVerificationPasswordRepository()
        {
            var verificationPasswords = new List<VerificationPassword>
            {
                new VerificationPassword
                {
                    VerificationId = 1,
                    UserId = 1,
                    OTPCode = "123456",
                    Expired = DateTime.Now.AddMinutes(-5)
                },
                new VerificationPassword
                {
                    VerificationId = 2,
                    UserId = 2,
                    OTPCode = "234567",
                    Expired = DateTime.Now.AddMinutes(5)
                },
            };

            var mockRepo = new Mock<IVerificationPasswordRepository>();

            mockRepo.Setup(x => x.GetOtpRecentlyByUserId(It.IsAny<int>()))
                .ReturnsAsync((int userId) => verificationPasswords.FirstOrDefault(x => x.UserId == userId));

            mockRepo.Setup(x => x.GenerateOTP(It.IsAny<int>()))
                .ReturnsAsync((int userId) =>
                {
                    var verify = new VerificationPassword
                    {
                        UserId = userId,
                        OTPCode = "123456",
                        Expired = DateTime.Now.AddMinutes(5)
                    };

                    verificationPasswords.Add(verify);
                    return verify.OTPCode;
                });





            return mockRepo;
        }
    }
}