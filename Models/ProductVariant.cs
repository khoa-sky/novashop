using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaShop.Models
{
    [Table("ProductVariants")]
    public class ProductVariant
    {
        [Key]
        public int VariantID { get; set; }

        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [StringLength(60)]
        public string SKU { get; set; }

        [Required]
        [StringLength(50)]
        public string ColorName { get; set; }

        [Required]
        [StringLength(20)]
        public string SizeName { get; set; } // S, M, L, XL

        public int StockQuantity { get; set; } = 0;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();

        [NotMapped]
        public string DisplayLabel => string.Format("{0} - Size {1} (Còn {2})", ColorName, SizeName, StockQuantity);
    }
}
