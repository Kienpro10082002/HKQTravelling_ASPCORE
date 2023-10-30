using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HKQTravelling.Models
{
    public class Rules
    {
        /**
         * Tuy cho tất cả các thuộc tính đều null nhưng khi đưa dữ liệu vào thì cần phải có 1 thuộc
         * tính là giá trị không null
        **/
        [Key]
        [Column("RULE_ID")]
        public long RuleId { get; set; }

        [Column("PRICE_INCLUDE")]
        [MaxLength(1000)]
        public string? PriceInclude { get; set; }

        [Column("PRICE_NOT_INCLUDE")]
        [MaxLength(1000)]
        public string? PriceNotInclude { get; set; }

        [Column("SURCHARGE")]
        [MaxLength(1000)]
        public string? Surcharge { get; set; }

        [Column("CANCLE_CHANGE")]
        [MaxLength(1000)]
        public string? CancelChange { get; set; }

        [Column("NOTE")]
        [MaxLength(1000)]
        public string? Note { get; set; }
    }
}
