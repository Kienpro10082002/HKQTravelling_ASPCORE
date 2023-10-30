using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HKQTravelling.Models
{
    public class Users
    {
        [Key]
        [Required]
        [Column("USER_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        [Required]
        [Column("USERNAME")]
        [MaxLength(256)]
        public string Username { get; set; } // Không được null

        [Required]
        [Column("PASSWORD")]
        [MaxLength(256)]
        public string Password { get; set; } // Không được null

        [Column("STATUS")]
        public int? Status { get; set; }

        [Column("CREATION_DATE")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // Tự động tạo ngày
        public DateTime? CreationDate { get; set; }

        [Column("UPDATE_DATE")]
        public DateTime? UpdateDate { get; set; }

        //Khóa ngoại
        [Column("ROLE_ID")]
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Roles roles { get; set; }
    }
}
