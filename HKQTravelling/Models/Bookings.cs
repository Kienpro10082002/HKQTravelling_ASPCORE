using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace HKQTravelling.Models
{
    public class Bookings
    {
        [Key]
        [Column("BOOKING_ID")]
        public long BookingId { get; set; }

        [Column("BOOKING_DATE")]
        public DateTime? BookingDate { get; set; }

        [Column("NUM_ADULTS")]
        public int? NumAdults { get; set; }

        [Column("NUM_TODDLERS")]
        public int? NumToddlers { get; set; }

        [Column("NUM_KIDS")]
        public int? NumKids { get; set; }

        [Column("PRICE_ADULTS")]
        public double? PriceAdults { get; set; }

        [Column("PRICE_TODDLERS")]
        public double? PriceToddlers { get; set; }

        [Column("PRICE_KIDS")]
        public double? PriceKids { get; set; }

        //Khóa ngoại

        [Column("TOUR_ID")]
        public long? TourId { get; set; }

        [ForeignKey("TourId")]
        public Tours tours{ get; set; }

        [Column("USER_ID")]
        public long? UserId { get; set; }

        [ForeignKey("UserId")]
        public Users users { get; set; }
    }
}
