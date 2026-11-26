using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NovaShop.Models;

namespace NovaShop.Services
{
    public static class OrderService
    {
        public static Order CreateOrderFromCart(int? userId, string customerName, string phone, string address, string note, List<CartItem> cartItems, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (cartItems == null || cartItems.Count == 0)
            {
                errorMessage = "Giỏ hàng của bạn đang trống.";
                return null;
            }

            using (var db = new ShopDbContext())
            {
                using (var tx = db.Database.BeginTransaction())
                {
                    try
                    {
                        // 1. Kiểm tra tồn kho thực tế trong DB trước khi tạo đơn
                        foreach (var item in cartItems)
                        {
                            var variant = db.ProductVariants
                                            .Include(v => v.Product)
                                            .FirstOrDefault(v => v.VariantID == item.VariantID);

                            if (variant == null)
                            {
                                errorMessage = string.Format("Sản phẩm '{0}' không còn tồn tại.", item.ProductName);
                                tx.Rollback();
                                return null;
                            }

                            if (variant.StockQuantity < item.Quantity)
                            {
                                errorMessage = string.Format("Sản phẩm '{0}' ({1} - Size {2}) chỉ còn {3} chiếc trong kho.",
                                    item.ProductName, item.ColorName, item.SizeName, variant.StockQuantity);
                                tx.Rollback();
                                return null;
                            }
                        }

                        // 2. Tạo bản ghi Orders
                        var order = new Order
                        {
                            UserID = userId,
                            CustomerName = customerName.Trim(),
                            CustomerPhone = phone.Trim(),
                            ShippingAddress = address.Trim(),
                            Note = (note ?? string.Empty).Trim(),
                            TotalAmount = cartItems.Sum(i => i.LineTotal),
                            OrderStatus = "Pending",
                            OrderDate = DateTime.Now
                        };

                        db.Orders.Add(order);
                        db.SaveChanges();

                        // 3. Tạo các bản ghi OrderDetails & trừ tồn kho ProductVariants
                        foreach (var item in cartItems)
                        {
                            var detail = new OrderDetail
                            {
                                OrderID = order.OrderID,
                                VariantID = item.VariantID,
                                Quantity = item.Quantity,
                                UnitPrice = item.UnitPrice
                            };
                            db.OrderDetails.Add(detail);

                            var variant = db.ProductVariants.First(v => v.VariantID == item.VariantID);
                            variant.StockQuantity -= item.Quantity;
                        }

                        db.SaveChanges();
                        tx.Commit();
                        return order;
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        errorMessage = "Có lỗi xảy ra khi xử lý đơn hàng: " + ex.Message;
                        return null;
                    }
                }
            }
        }

        public static List<Order> GetOrdersByUser(int userId)
        {
            using (var db = new ShopDbContext())
            {
                return db.Orders
                         .Include(o => o.OrderDetails.Select(od => od.Variant.Product))
                         .Where(o => o.UserID == userId)
                         .OrderByDescending(o => o.OrderDate)
                         .ToList();
            }
        }

        public static bool CancelOrderByCustomer(int orderId, int userId, out string message)
        {
            message = string.Empty;
            using (var db = new ShopDbContext())
            {
                var order = db.Orders
                              .Include(o => o.OrderDetails)
                              .FirstOrDefault(o => o.OrderID == orderId && o.UserID == userId);

                if (order == null)
                {
                    message = "Không tìm thấy đơn hàng.";
                    return false;
                }

                if (order.OrderStatus != "Pending")
                {
                    message = "Chỉ có thể hủy đơn hàng khi đang ở trạng thái 'Chờ xử lý'.";
                    return false;
                }

                order.OrderStatus = "Cancelled";

                // Hoàn lại số lượng tồn kho
                foreach (var detail in order.OrderDetails)
                {
                    var variant = db.ProductVariants.FirstOrDefault(v => v.VariantID == detail.VariantID);
                    if (variant != null)
                    {
                        variant.StockQuantity += detail.Quantity;
                    }
                }

                db.SaveChanges();
                message = "Đã hủy đơn hàng #" + orderId + " thành công.";
                return true;
            }
        }

        public static bool UpdateOrderStatusByAdmin(int orderId, string newStatus, out string message)
        {
            message = string.Empty;
            using (var db = new ShopDbContext())
            {
                var order = db.Orders
                              .Include(o => o.OrderDetails)
                              .FirstOrDefault(o => o.OrderID == orderId);

                if (order == null)
                {
                    message = "Không tìm thấy đơn hàng.";
                    return false;
                }

                string oldStatus = order.OrderStatus;
                if (oldStatus == newStatus)
                {
                    message = "Trạng thái đơn hàng không thay đổi.";
                    return true;
                }

                // Nếu chuyển từ trạng thái khác sang Cancelled -> hoàn lại tồn kho
                if (oldStatus != "Cancelled" && newStatus == "Cancelled")
                {
                    foreach (var detail in order.OrderDetails)
                    {
                        var variant = db.ProductVariants.FirstOrDefault(v => v.VariantID == detail.VariantID);
                        if (variant != null) variant.StockQuantity += detail.Quantity;
                    }
                }
                // Nếu khôi phục từ Cancelled sang trạng thái khác -> trừ lại tồn kho
                else if (oldStatus == "Cancelled" && newStatus != "Cancelled")
                {
                    foreach (var detail in order.OrderDetails)
                    {
                        var variant = db.ProductVariants.FirstOrDefault(v => v.VariantID == detail.VariantID);
                        if (variant != null) variant.StockQuantity = Math.Max(0, variant.StockQuantity - detail.Quantity);
                    }
                }

                order.OrderStatus = newStatus;
                db.SaveChanges();
                message = string.Format("Đã cập nhật trạng thái đơn #{0} sang '{1}'.", orderId, order.StatusVietnamese);
                return true;
            }
        }
    }
}
