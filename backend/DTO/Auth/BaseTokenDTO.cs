namespace backend.DTO.Auth
{
    public class BaseTokenDTO
    {
        public string Type { get; set; }
        public string Access { get; set; }
        public string Refresh { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}
