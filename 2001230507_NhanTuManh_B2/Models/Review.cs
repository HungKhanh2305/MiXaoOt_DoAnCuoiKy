namespace _2001230507_NhanTuManh_B2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        public int ReviewID { get; set; }

        public int CustomerID { get; set; }

        public int? ProductID { get; set; }

        public int? OrderID { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime? ReviewDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
