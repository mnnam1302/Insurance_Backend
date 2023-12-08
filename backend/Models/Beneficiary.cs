using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    [Table("beneficiaries")]
    public class Beneficiary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("beneficiary_id")]
        public int BeneficiaryId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; } = "";

        [Column("full_name")]
        [StringLength(255)]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Column("phone")]
        [StringLength(20)]
        public string Phone { get; set; } = "";

        [Column("sex")]
        [StringLength(5)]
        public string? Sex { get; set; }

        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Card Identification is required")]
        [Column("card_identification")]
        [StringLength(20)]
        public string CardIdentification { get; set; } = "";

        [Column("image_identification_url")]
        [StringLength(255)]
        public string? ImageIdentificationUrl { get; set; }

        [Column("address")]
        [StringLength(255)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Relationship policyholder is required")]
        [Column("relationship_policyholder")]
        [StringLength(100)]
        public string RelationshipPolicyholder { get; set; } = "";

        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
