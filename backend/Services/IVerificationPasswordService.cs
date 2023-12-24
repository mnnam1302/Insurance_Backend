using backend.Models;
using Firebase.Auth;

namespace backend.Services
{
    public interface IVerificationPasswordService
    {
        Task<string> GenerateOTP(int userId);

        Task VerificationOTP(int userId, string otp);

        Task ResetPassword(int userId, string newPassword);
    }
}
