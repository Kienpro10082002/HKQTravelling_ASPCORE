using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HKQTravelling.Models
{
    public class TourImages
    {
        [Key]
        [Column("IMAGE_ID")]
        public long ImageId { get; set; }

        [Column("IMAGE_URL")]
        public string ImageUrl { get; set; }

        [Column("DAY_NUMBER")]
        public int? DayNumber { get; set; }

        [Column("TOUR_ID")]
        public long? TourId { get; set; }

        [ForeignKey("TourId")]
        public Tours tours { get; set; }
    }
}
