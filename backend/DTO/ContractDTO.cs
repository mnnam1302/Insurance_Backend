using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class ContractDTO
    {
        [Column("contract_id")]
        public int contract_id { get; set; }

        [Column("insurance_code")]
        public string insurance_code { get; set; } = "";

        [Column("signing_Date")]
        //[Required]
        public DateTime? signing_date { get; set; }

        [Column("start_Date")]
        //[Required]
        public DateTime? start_date { get; set; }

        [Column("end_Date")]
        //[Required]
        public DateTime? end_date { get; set; }

        [Column("total_Turn")]
        //[Required]
        public int total_turn { get; set; }

        [Column("contract_Status")]
        //[Required]
        public string contract_status { get; set; } = "";

        [Column("initial_Fee")]
        //[Required]
        public decimal initial_fee_per_turn { get; set; }

        [Column("bonus_Fee")]
        //[Required]
        public decimal bonus_fee { get; set; } = 0;

        [Column("discount")]
        //[Required]
        public decimal discount { get; set; }

        [Column("periodic_Fee")]
        //[Required]
        public decimal periodic_fee { get; set; }

        [Column("total_Fee")]
        //[Required]
        public decimal total_fee { get; set; }

        [Column("user_id")]
        public int user_id { get; set; }

        [Column("beneficiary_id")]
        //[Required]
        public int beneficial_id { get; set; }

        [Column("insurance_id")]
        //[Required]
        public int insurance_id { get; set; }

        [Column("registration_id")]
        [Required]
        public int registration_id { get; set; }
    }
}
