using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HKQTravelling.Models
{
    public class Payments
    {
        [Key]
        [Column("PAYMENT_ID")]
        public long PaymentId { get; set; }

        [Column("AMOUNT")]
        public int? Amount { get; set; }

        [Column("PAYMENT_DATE")]
        public DateTime? PaymentDate { get; set; }

        [Column("BOOKING_ID")]
        public long? BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Bookings bookings { get; set; }
    }
}
