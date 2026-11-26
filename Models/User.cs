using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaShop.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [StringLength(128)]
        public string PasswordHash { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "Customer"; // "Customer" | "Admin"

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
