using backend.Models;
using Firebase.Auth;

namespace backend.IRepositories
{
    public interface IVerificationPasswordRepository
    {
        Task<string> GenerateOTP(int userId);

        Task<VerificationPassword> GetOtpRecentlyByUserId(int userId);

        Task ResetPassword(int userId, string newPassword);
    }
}
