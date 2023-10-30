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

        //Khóa ngoại
        [Column("TICKET_INFO_ID")]
        public long? TicketInfoId { get; set; }

        [ForeignKey("TicketInfoId")]
        public TicketInformations ticketInformations { get; set; }

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
