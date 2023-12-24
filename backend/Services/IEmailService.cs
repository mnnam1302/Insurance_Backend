using backend.DTO;

namespace backend.Services
{
    public interface IEmailService
    {
        Task SendEmaiAsync(EmailRequestDTO mailRequest);
    }
}
