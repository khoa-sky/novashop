using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaShop.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(120)]
        public string Slug { get; set; }

        public int? ParentID { get; set; }

        [ForeignKey("ParentID")]
        public virtual Category ParentCategory { get; set; }

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
