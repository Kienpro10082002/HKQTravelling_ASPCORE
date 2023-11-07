using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HKQTravelling.Models
{
    public class UserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("USER_DETAIL_ID")]
        public long UserDetailId { get; set; }

        [Column("GENDER")]
        public int? Gender { get; set; }

        [Column("BIRTHDATE")]
        public DateTime? Birthdate { get; set; }

        [Column("AGE")]
        public int? Age { get; set; }

        [Column("SURNAME")]
        [StringLength(30)]
        public string Surname { get; set; }

        [Column("NAME")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("EMAIL")]
        [MaxLength(256)]
        public string? Email { get; set; }

        [Required]
        [Column("PHONE_NUMBER")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Column("NI_NUMBER")]
        [StringLength(12)]
        public string? NiNumber { get; set; }

        [Column("IMAGE_URL")]
        public string? ImageUrl { get; set; }

        //Khóa ngoại
        [Column("USER_ID")]
        public long? UserId { get; set; }

        [ForeignKey("UserId")]
        public Users users { get; set; }
    }
}
