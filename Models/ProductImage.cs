using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaShop.Models
{
    [Table("ProductImages")]
    public class ProductImage
    {
        [Key]
        public int ImageID { get; set; }

        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; }

        public int SortOrder { get; set; } = 0;
    }
}
