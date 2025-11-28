namespace _2001230507_NhanTuManh_B2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Promotion
    {
        public int PromotionID { get; set; }

        [Required]
        [StringLength(50)]
        public string PromotionCode { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string DiscountType { get; set; }

        public decimal DiscountValue { get; set; }

        public decimal? MinimumOrderAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? UsageLimit { get; set; }

        public int? UsedCount { get; set; }

        public bool? IsActive { get; set; }
    }
}
