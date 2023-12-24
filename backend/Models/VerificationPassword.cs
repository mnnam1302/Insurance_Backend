using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("verification_password")]
    public class VerificationPassword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("verification_id")]
        public int VerificationId { get; set; }

        [Column("otp_code")]
        public string OTPCode { get; set; } = "";

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("expired")]
        public DateTime Expired { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
