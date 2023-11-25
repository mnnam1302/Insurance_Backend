using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{

    [Table("tokens")]
    public class Token
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("token_id")]
        public int TokenId { get; set; }

        [Required]
        [Column("token")]
        [StringLength(255)]
        public string TokenValue { get; set; } = "";

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("expired")]
        public DateTime Expired { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
