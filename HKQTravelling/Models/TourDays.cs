using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HKQTravelling.Models
{
    public class TourDays
    {
        [Key]
        [Column("TOUR_DAY_ID")]
        public long TourDayId { get; set; }

        [Column("DAY_NUMBER")]
        public int? DayNumber { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("DESTINATION")]
        [MaxLength(100)]
        public string Destination { get; set; }

        [Column("TIME_SCHEDULE")]
        public DateTime? TimeSchedule { get; set; }

        //Khóa ngoại
        [Column("TOUR_ID")]
        public long? TourId { get; set; }

        [ForeignKey("TourId")]
        public Tours tours { get; set; }
    }
}
