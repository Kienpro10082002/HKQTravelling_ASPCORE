using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace HKQTravelling.Models
{
    public class TicketInformations
    {
        [Key]
        [Column("TICKET_INFO_ID")]
        public long TicketInfoId { get; set; }

        [Column("ADULTS_NUMBER")]
        public int? AdultsNumber { get; set; }

        [Column("KID_NUMBER")]
        public int? KidNumber { get; set; }

        [Column("KID_AGE")]
        public int? KidAge { get; set; }

        //Khóa ngoại
        [Column("TICKET_TYPE_ID")]
        public int? TicketTypeId { get; set; }

        [ForeignKey("TicketTypeId")]
        public TicketTypes tickeTypes { get; set; }

        [Column("USER_ID")]
        public long? UserId { get; set; }

        [ForeignKey("UserId")]
        public Users users { get; set; }
    }

}
