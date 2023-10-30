namespace HKQTravelling.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Roles
    {
        [Key]
        [Required]
        [Column("ROLE_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Required]
        [Column("ROLE_NAME")]
        [MaxLength(100)]
        public string RoleName { get; set; }
    }
}
