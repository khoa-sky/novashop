using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaShop.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public decimal? SalePrice { get; set; }

        [StringLength(500)]
        public string Thumbnail { get; set; }

        public string Description { get; set; }

        public bool IsFeatured { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<ProductVariant> Variants { get; set; } = new HashSet<ProductVariant>();

        public virtual ICollection<ProductImage> Images { get; set; } = new HashSet<ProductImage>();

        [NotMapped]
        public decimal EffectivePrice => SalePrice.HasValue && SalePrice.Value > 0 && SalePrice.Value < Price
            ? SalePrice.Value
            : Price;

        [NotMapped]
        public bool HasSale => SalePrice.HasValue && SalePrice.Value > 0 && SalePrice.Value < Price;

        [NotMapped]
        public int DiscountPercent => HasSale && Price > 0
            ? (int)Math.Round((1 - (SalePrice.Value / Price)) * 100)
            : 0;
    }
}
