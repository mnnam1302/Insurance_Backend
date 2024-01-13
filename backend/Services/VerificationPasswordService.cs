using backend.IRepositories;
using backend.Models;

namespace backend.Services
{
    public class VerificationPasswordService : IVerificationPasswordService
    {
        private readonly IVerificationPasswordRepository _verificationPasswordRepository;

        public VerificationPasswordService(IVerificationPasswordRepository verificationPasswordRepository)
        {
            _verificationPasswordRepository = verificationPasswordRepository;
        }


        public async Task<string> GenerateOTP(int userId)
        {
            return await _verificationPasswordRepository.GenerateOTP(userId);
        }

        public async Task ResetPassword(int userId, string newPassword)
        {
            await _verificationPasswordRepository.ResetPassword(userId, newPassword);
            return;
        }

        public async Task VerificationOTP(int userId, string otp)
        {
            // Lấy verifiction lên check
            VerificationPassword verification = await _verificationPasswordRepository.GetOtpRecentlyByUserId(userId);

            if (verification == null)
            {
                throw new Exception("User's verification OTP is not valid");
            }
            
            // Check thời gian xem hết hạn
            if (verification.Expired < DateTime.Now)
            {
                throw new Exception("OTP code is expired");
            }
            // Check OTP có đúng không
            if (!verification.OTPCode.Equals(otp))
            {
                throw new Exception("OTP is not correct");
            }
            return;
        }
    }
}
