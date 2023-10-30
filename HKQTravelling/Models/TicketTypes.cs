using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HKQTravelling.Models
{
    public class TicketTypes
    {
        [Key]
        [Required]
        [Column("TICKET_TYPE_ID")]
        public int TicketTypeId { get; set; }

        [Required]
        [Column("TYPE_NAME")]
        [MaxLength(100)]
        public string TypeName { get; set; }
    }
}
