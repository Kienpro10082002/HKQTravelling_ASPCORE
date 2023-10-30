using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace HKQTravelling.Models
{
    public class Tours
    {
        [Key]
        [Column("TOUR_ID")]
        public long TourId { get; set; }

        [Column("TOUR_NAME")]
        [MaxLength(200)]
        public string TourName { get; set; }

        [Column("PRICE")]
        public int? Price { get; set; }

        [Column("START_DATE")]
        public DateTime? StartDate { get; set; }

        [Column("END_DATE")]
        public DateTime? EndDate { get; set; }

        [Column("STATUS")]
        public int? Status { get; set; }

        [Column("CREATION_DATE")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreationDate { get; set; }

        [Column("UPDATE_DATE")]
        public DateTime? UpdateDate { get; set; }

        [Column("REMAINING")]
        public int? Remaining { get; set; }

        //Khóa ngoại
        [Column("DIS_ID")]
        public long? DiscountId { get; set; }

        [ForeignKey("DiscountId")]
        public Discounts discounts { get; set; }

        [Column("START_LOCATION_ID")]
        public long? StartLocationId { get; set; }

        [ForeignKey("StartLocationId")]
        public StartLocations startLocations { get; set; }

        [Column("END_LOCATION_ID")]
        public long? EndLocationId { get; set; }

        [ForeignKey("EndLocationId")]
        public EndLocations endLocations { get; set; }

        [Column("RULE_ID")]
        public long? RuleId { get; set; }

        [ForeignKey("RuleId")]
        public Rules rules { get; set; }
    }
}
