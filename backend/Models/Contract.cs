﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    [Table("contracts")]
    public class Contract
    {
        [Column("contract_id")]
        [Key] public int contract_id { get; set; }

        [Column("insurance_code")]
        public string insurance_code { get; set; } = "";

        [Column("signing_Date")]
        public DateTime? signing_date { get; set; }

        [Column("start_Date")]
        //[Required]
        public DateTime? start_date { get; set; }

        [Column("end_Date")]
        //[Required]
        public DateTime? end_date { get; set; }

        [Column("total_Turn")]
        public int total_turn { get; set; }

        [Column("contract_Status")]
        public string contract_status { get; set; } = "";

        [Column("initial_Fee")]
        public decimal initial_fee_per_turn { get; set; }

        [Column("bonus_Fee")]
        public decimal bonus_fee { get; set; } = 0;

        [Column("discount")]
        public decimal discount { get; set; }

        [Column("periodic_Fee")]
        public decimal periodic_fee { get; set; }

        [Column("total_Fee")]
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

        [ForeignKey("user_id")]
        public User? user { get; set; }

        [ForeignKey("beneficial_id")]
        public Beneficiary? beneficiary { get; set; }

        [ForeignKey("registration_id")]
        public Registration? registration { get; set; }

        [ForeignKey("insurance_id")]
        public Insurance? insurance { get; set; }
    }
}