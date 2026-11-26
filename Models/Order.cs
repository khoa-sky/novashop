using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaShop.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public int? UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(20)]
        public string CustomerPhone { get; set; }

        [Required]
        [StringLength(300)]
        public string ShippingAddress { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(30)]
        public string OrderStatus { get; set; } = "Pending"; // Pending | Shipping | Completed | Cancelled

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();

        [NotMapped]
        public string StatusVietnamese
        {
            get
            {
                switch (OrderStatus)
                {
                    case "Pending": return "Chờ xử lý";
                    case "Shipping": return "Đang giao hàng";
                    case "Completed": return "Hoàn thành";
                    case "Cancelled": return "Đã hủy";
                    default: return OrderStatus;
                }
            }
        }
    }
}
