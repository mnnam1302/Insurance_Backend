using backend.DTO.Email;

namespace backend.Services
{
    public interface IEmailService
    {
        Task SendEmaiAsync(EmailRequestDTO mailRequest);
    }
}
