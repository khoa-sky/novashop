using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaShop.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        public int VariantID { get; set; }

        [ForeignKey("VariantID")]
        public virtual ProductVariant Variant { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        [NotMapped]
        public decimal LineTotal => Quantity * UnitPrice;
    }
}
