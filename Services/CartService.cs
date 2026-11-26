using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NovaShop.Models;

namespace NovaShop.Services
{
    public static class CartService
    {
        private const string SessionCartKey = "Cart";

        public static List<CartItem> GetCart()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
                return new List<CartItem>();

            var cart = HttpContext.Current.Session[SessionCartKey] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                HttpContext.Current.Session[SessionCartKey] = cart;
            }
            return cart;
        }

        public static int GetTotalQuantity()
        {
            return GetCart().Sum(i => i.Quantity);
        }

        public static decimal GetTotalAmount()
        {
            return GetCart().Sum(i => i.LineTotal);
        }

        public static bool AddToCart(int variantId, int quantity, out string message)
        {
            message = string.Empty;
            if (quantity <= 0) quantity = 1;

            using (var db = new ShopDbContext())
            {
                var variant = db.ProductVariants
                                .Include(v => v.Product)
                                .FirstOrDefault(v => v.VariantID == variantId);

                if (variant == null || variant.Product == null || !variant.Product.IsActive)
                {
                    message = "Biến thể sản phẩm không tồn tại hoặc đã ngừng kinh doanh.";
                    return false;
                }

                if (variant.StockQuantity <= 0)
                {
                    message = "Màu và Size này hiện đã hết hàng.";
                    return false;
                }

                var cart = GetCart();
                var existing = cart.FirstOrDefault(c => c.VariantID == variantId);

                int newQty = (existing != null ? existing.Quantity : 0) + quantity;
                if (newQty > variant.StockQuantity)
                {
                    message = string.Format("Số lượng tồn kho chỉ còn {0} sản phẩm cho {1} - Size {2}.",
                        variant.StockQuantity, variant.ColorName, variant.SizeName);
                    return false;
                }

                if (existing != null)
                {
                    existing.Quantity = newQty;
                    existing.MaxStock = variant.StockQuantity;
                    existing.UnitPrice = variant.Product.EffectivePrice;
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        VariantID = variant.VariantID,
                        ProductID = variant.ProductID,
                        ProductName = variant.Product.ProductName,
                        Thumbnail = variant.Product.Thumbnail,
                        ColorName = variant.ColorName,
                        SizeName = variant.SizeName,
                        UnitPrice = variant.Product.EffectivePrice,
                        Quantity = quantity,
                        MaxStock = variant.StockQuantity
                    });
                }

                HttpContext.Current.Session[SessionCartKey] = cart;
                message = "Đã thêm sản phẩm vào giỏ hàng.";
                return true;
            }
        }

        public static void UpdateQuantity(int variantId, int newQuantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.VariantID == variantId);
            if (item == null) return;

            if (newQuantity <= 0)
            {
                cart.Remove(item);
            }
            else
            {
                item.Quantity = Math.Min(newQuantity, item.MaxStock > 0 ? item.MaxStock : newQuantity);
            }

            HttpContext.Current.Session[SessionCartKey] = cart;
        }

        public static void RemoveItem(int variantId)
        {
            var cart = GetCart();
            cart.RemoveAll(c => c.VariantID == variantId);
            HttpContext.Current.Session[SessionCartKey] = cart;
        }

        public static void ClearCart()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Remove(SessionCartKey);
            }
        }
    }
}
