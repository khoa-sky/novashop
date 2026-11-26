using System;

namespace NovaShop.Models
{
    [Serializable]
    public class CartItem
    {
        public int VariantID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Thumbnail { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int MaxStock { get; set; }

        public decimal LineTotal => UnitPrice * Quantity;
    }
}
