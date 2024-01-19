
namespace backend.DTO.User
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string CardIdentification { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
