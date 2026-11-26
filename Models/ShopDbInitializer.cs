using System;
using System.Collections.Generic;
using System.Data.Entity;
using NovaShop.Services;

namespace NovaShop.Models
{
    public class ShopDbInitializer : CreateDatabaseIfNotExists<ShopDbContext>
    {
        protected override void Seed(ShopDbContext context)
        {
            // 1. Tạo tài khoản Admin & Khách hàng mẫu (mật khẩu chung: 123456)
            string defaultHash = AuthService.HashPassword("123456");

            var adminUser = new User
            {
                FullName = "Quản Trị Viên NOVA",
                Email = "admin@nova.vn",
                PasswordHash = defaultHash,
                Phone = "0909123456",
                Address = "770F Sư Vạn Hạnh, Quận 10, TP.HCM",
                Role = "Admin",
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            var customerUser = new User
            {
                FullName = "Nguyễn Văn Khách",
                Email = "khach@gmail.com",
                PasswordHash = defaultHash,
                Phone = "0988777666",
                Address = "123 Cầu Giấy, Quận Cầu Giấy, Hà Nội",
                Role = "Customer",
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            context.Users.Add(adminUser);
            context.Users.Add(customerUser);
            context.SaveChanges();

            // 2. Tạo 8 Danh mục chuẩn thanh Menu NOVA.VN
            var catAoThun = new Category { CategoryName = "ÁO THUN", Slug = "ao-thun", DisplayOrder = 1, IsActive = true };
            var catAoSoMi = new Category { CategoryName = "ÁO SƠ MI", Slug = "ao-so-mi", DisplayOrder = 2, IsActive = true };
            var catAoKhoac = new Category { CategoryName = "ÁO KHOÁC", Slug = "ao-khoac", DisplayOrder = 3, IsActive = true };
            var catQuanJeans = new Category { CategoryName = "QUẦN JEANS", Slug = "quan-jeans", DisplayOrder = 4, IsActive = true };
            var catQuanDai = new Category { CategoryName = "QUẦN DÀI", Slug = "quan-dai", DisplayOrder = 5, IsActive = true };
            var catQuanTay = new Category { CategoryName = "QUẦN TÂY", Slug = "quan-tay", DisplayOrder = 6, IsActive = true };
            var catQuanShort = new Category { CategoryName = "QUẦN SHORT", Slug = "quan-short", DisplayOrder = 7, IsActive = true };
            var catPhuKien = new Category { CategoryName = "PHỤ KIỆN", Slug = "phu-kien", DisplayOrder = 8, IsActive = true };

            context.Categories.AddRange(new[]
            {
                catAoThun, catAoSoMi, catAoKhoac, catQuanJeans, catQuanDai, catQuanTay, catQuanShort, catPhuKien
            });
            context.SaveChanges();

            // 3. Tạo 20 sản phẩm quần áo mẫu kèm Biến thể Size (S, M, L, XL) và Màu sắc
            var sampleProducts = new List<Product>
            {
                // ÁO THUN
                new Product
                {
                    CategoryID = catAoThun.CategoryID,
                    ProductName = "Áo Thun Cổ Tròn CoolTouch Kháng Nhăn Form F1",
                    Price = 250000,
                    SalePrice = 199000,
                    Thumbnail = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=800&q=80",
                    Description = "Thiết kế cơ bản nhưng chiều sâu không hề đơn giản: Chất vải chạm mát CoolTouch, co giãn 4 chiều kháng nhăn, chuẩn hóa hệ phom F1 cho thể trạng người Việt.",
                    IsFeatured = true,
                    IsActive = true
                },
                new Product
                {
                    CategoryID = catAoThun.CategoryID,
                    ProductName = "Áo Thun Polo Dệt Kim Thu Đông Unisex Form F2",
                    Price = 350000,
                    SalePrice = 279000,
                    Thumbnail = "https://images.unsplash.com/photo-1586363104862-3a5e2ab60d99?auto=format&fit=crop&w=800&q=80",
                    Description = "Áo Polo dệt kim sợi bông tự nhiên mềm mịn, giữ phom cổ đứng lịch sự, nam nữ mặc đều tôn dáng.",
                    IsFeatured = true,
                    IsActive = true
                },
                new Product
                {
                    CategoryID = catAoThun.CategoryID,
                    ProductName = "Áo Thun Oversize Streetwear Essential Cotton 250GSM",
                    Price = 290000,
                    SalePrice = null,
                    Thumbnail = "https://images.unsplash.com/photo-1583743814966-8936f5b7be1a?auto=format&fit=crop&w=800&q=80",
                    Description = "Định lượng vải dày dặn 250GSM 100% Cotton 2 chiều, thấm hút mồ hôi vượt trội, bo cổ dày không dão.",
                    IsFeatured = true,
                    IsActive = true
                },
                new Product
                {
                    CategoryID = catAoThun.CategoryID,
                    ProductName = "Áo Thun Boxy Basic Black Friday Special",
                    Price = 190000,
                    SalePrice = 39000,
                    Thumbnail = "https://images.unsplash.com/photo-1503342217505-b0a15ec3261c?auto=format&fit=crop&w=800&q=80",
                    Description = "Phiên bản ưu đãi Black Friday đồng giá từ 39K. Chất thun mềm nhẹ, dễ phối layer cùng sơ mi hoặc áo khoác.",
                    IsFeatured = true,
                    IsActive = true
                },

                // ÁO SƠ MI
                new Product
                {
                    CategoryID = catAoSoMi.CategoryID,
                    ProductName = "Áo Sơ Mi Dài Tay Oxford Minimalist Không Nhăn",
                    Price = 390000,
                    SalePrice = 312000,
                    Thumbnail = "https://images.unsplash.com/photo-1596755094514-f87e34085b2c?auto=format&fit=crop&w=800&q=80",
                    Description = "Sơ mi Oxford cao cấp được xử lý chống nhăn tự nhiên, form Regular vừa vặn lịch lãm cho công sở lẫn dạo phố.",
                    IsFeatured = true,
                    IsActive = true
                },
                new Product
                {
                    CategoryID = catAoSoMi.CategoryID,
                    ProductName = "Áo Sơ Mi Tay Ngắn Cuban Collar Linen Thoáng Mát",
                    Price = 340000,
                    SalePrice = null,
                    Thumbnail = "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?auto=format&fit=crop&w=800&q=80",
                    Description = "Chất liệu Linen pha Bamboo thoáng khí, cổ Cuban phóng khoáng thích hợp cho mùa hè và các chuyến du lịch.",
                    IsFeatured = false,
                    IsActive = true
                },
                new Product
                {
                    CategoryID = catAoSoMi.CategoryID,
                    ProductName = "Áo Sơ Mi Flannel Kẻ Caro Layering Unisex",
                    Price = 420000,
                    SalePrice = 349000,
                    Thumbnail = "https://images.unsplash.com/photo-1589310243389-96a5483213a8?auto=format&fit=crop&w=800&q=80",
                    Description = "Vải dạ Flannel mềm ấm, họa tiết caro cổ điển, mặc khoác ngoài áo thun trắng cực kỳ cuốn hút.",
                    IsFeatured = false,
                    IsActive = true
                },

                // ÁO KHOÁC
                new Product
                {
                    CategoryID = catAoKhoac.CategoryID,
                    ProductName = "Áo Khoác Gió Парka Phối Màu Outdoor Trượt Nước",
                    Price = 550000,
                    SalePrice = 440000,
                    Thumbnail = "https://images.unsplash.com/photo-1551028719-00167b16eac5?auto=format&fit=crop&w=800&q=80",
                    Description = "Thiết kế Outdoor 2 lớp trượt nước, chống tia UV UPF50+, mũ trùm đầu rộng rãi kèm khóa kéo chống gió.",
                    IsFeatured = true,
                    IsActive = true
                },
                new Product
                {
                    CategoryID = catAoKhoac.CategoryID,
                    ProductName = "Áo Khoác Hoodie Nỉ Bông Heavyweight Form Rộng",
                    Price = 490000,
                    SalePrice = 392000,
                    Thumbnail = "https://images.unsplash.com/photo-1556821840-3a63f95609a7?auto=format&fit=crop&w=800&q=80",
                    Description = "Nỉ bông định lượng 380GSM giữ ấm tối ưu, mũ 2 lớp đứng form chuẩn phong cách Hàn Quốc.",
                    IsFeatured = true,
                    IsActive = true
                },
                new Product
                {
                    CategoryID = catAoKhoac.CategoryID,
                    ProductName = "Áo Khoác Bomber Da Lộn Suede Lịch Lãm",
                    Price = 650000,
                    SalePrice = null,
                    Thumbnail = "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?auto=format&fit=crop&w=800&q=80",
                    Description = "Chất liệu da lộn nhân tạo cao cấp kèm lớp lót dù mềm mượt, bo tay và gấu áo dệt kim bền bỉ.",
                    IsFeatured = false,
                    IsActive = true
                },

                // QUẦN JEANS
                new Product
                {
                    CategoryID = catQuanJeans.CategoryID,
                    ProductName = "Quần Jeans Nam Slimfit Denim Co Giãn F3",
                    Price = 480000,
                    SalePrice = 384000,
                    Thumbnail = "https://images.unsplash.com/photo-1542272604-787c3835535d?auto=format&fit=crop&w=800&q=80",
                    Description = "Vải Denim 12Oz pha 2% Spandex co giãn thoải mái khi vận động, wash màu xanh Indigo hiện đại.",
                    IsFeatured = true,
                    IsActive = true
                },
                new Product
                {
                    CategoryID = catQuanJeans.CategoryID,
                    ProductName = "Quần Jeans Ống Suông Straight Vintage Unisex",
                    Price = 520000,
                    SalePrice = null,
                    Thumbnail = "https://images.unsplash.com/photo-1541099649105-f69ad21f3246?auto=format&fit=crop&w=800&q=80",
                    Description = "Phom ống suông Straight che khuyết điểm chân hoàn hảo, phù hợp cho cả nam và nữ.",
                    IsFeatured = false,
                    IsActive = true
                },

                // QUẦN DÀI
                new Product
                {
                    CategoryID = catQuanDai.CategoryID,
                    ProductName = "Quần Dài Cargo Túi Hộp Dù Gió Outdoor",
                    Price = 420000,
                    SalePrice = 336000,
                    Thumbnail = "https://images.unsplash.com/photo-1517445312882-bc9910d016b7?auto=format&fit=crop&w=800&q=80",
                    Description = "Quần Cargo 6 túi tiện lợi, chất vải Kaki pha Nylon nhanh khô, thích hợp dã ngoại và dạo phố.",
                    IsFeatured = true,
                    IsActive = true
                },
                new Product
                {
                    CategoryID = catQuanDai.CategoryID,
                    ProductName = "Quần Jogger Thun Thể Thao Co Giãn 4 Chiều",
                    Price = 350000,
                    SalePrice = 280000,
                    Thumbnail = "https://images.unsplash.com/photo-1552902865-b72c031ac5ea?auto=format&fit=crop&w=800&q=80",
                    Description = "Quần Jogger bo gấu năng động, túi có khóa kéo an toàn khi chạy bộ hoặc tập luyện.",
                    IsFeatured = false,
                    IsActive = true
                },

                // QUẦN TÂY
                new Product
                {
                    CategoryID = catQuanTay.CategoryID,
                    ProductName = "Quần Tây Smart-Casual Lưng Thun Ẩn Chống Nhăn",
                    Price = 450000,
                    SalePrice = 360000,
                    Thumbnail = "https://images.unsplash.com/photo-1594633312681-425c7b97ccd1?auto=format&fit=crop&w=800&q=80",
                    Description = "Thiết kế lưng thun ẩn thông minh tự co giãn 3-4cm vòng bụng, giữ ly quần sắc nét cả ngày.",
                    IsFeatured = true,
                    IsActive = true
                },
                new Product
                {
                    CategoryID = catQuanTay.CategoryID,
                    ProductName = "Quần Tây Sidetab Classic Tailor Form F4",
                    Price = 490000,
                    SalePrice = null,
                    Thumbnail = "https://images.unsplash.com/photo-1506629082955-511b1aa562c8?auto=format&fit=crop&w=800&q=80",
                    Description = "Quần âu khóa Sidetab hai bên hông không cần dùng thắt lưng, tôn dáng lịch thiệp.",
                    IsFeatured = false,
                    IsActive = true
                },

                // QUẦN SHORT
                new Product
                {
                    CategoryID = catQuanShort.CategoryID,
                    ProductName = "Quần Short Kaki Co Giãn Basic Dạo Phố",
                    Price = 280000,
                    SalePrice = 224000,
                    Thumbnail = "https://images.unsplash.com/photo-1591195853828-11db59a44f6b?auto=format&fit=crop&w=800&q=80",
                    Description = "Quần short ngang gối trẻ trung, chất Kaki cotton thoáng mát đã qua xử lý mềm vải.",
                    IsFeatured = false,
                    IsActive = true
                },
                new Product
                {
                    CategoryID = catQuanShort.CategoryID,
                    ProductName = "Quần Short Thể Thao Nhanh Khô AirFlow",
                    Price = 220000,
                    SalePrice = 175000,
                    Thumbnail = "https://images.unsplash.com/photo-1565084888279-aca607ecce0c?auto=format&fit=crop&w=800&q=80",
                    Description = "Trọng lượng siêu nhẹ chỉ 140g, lỗ thoáng khí laser giúp khô nhanh gấp 3 lần vải thường.",
                    IsFeatured = false,
                    IsActive = true
                },

                // PHỤ KIỆN
                new Product
                {
                    CategoryID = catPhuKien.CategoryID,
                    ProductName = "Nón Bucket Outdoor Vành Rộng Chống Nắng",
                    Price = 180000,
                    SalePrice = 144000,
                    Thumbnail = "https://images.unsplash.com/photo-1556306535-0f09a537f0a3?auto=format&fit=crop&w=800&q=80",
                    Description = "Nón Bucket phong cách camping dã ngoại như bộ sưu tập Thu Đông NOVA, có dây rút cố định.",
                    IsFeatured = true,
                    IsActive = true
                },
                new Product
                {
                    CategoryID = catPhuKien.CategoryID,
                    ProductName = "Túi Đeo Chéo Canvas Minimalist Chống Thấm",
                    Price = 320000,
                    SalePrice = 256000,
                    Thumbnail = "https://images.unsplash.com/photo-1548036328-c9fa89d128fa?auto=format&fit=crop&w=800&q=80",
                    Description = "Túi đeo chéo vải Canvas phủ sáp trượt nước, ngăn chứa vừa iPad 11 inch và phụ kiện cá nhân.",
                    IsFeatured = true,
                    IsActive = true
                }
            };

            context.Products.AddRange(sampleProducts);
            context.SaveChanges();

            // 4. Khởi tạo các biến thể Size (S, M, L, XL) và Màu sắc cho từng sản phẩm
            string[] defaultColors = { "Đen", "Trắng Kem", "Be Cát" };
            string[] defaultSizes = { "S", "M", "L", "XL" };

            var allVariants = new List<ProductVariant>();
            int skuCounter = 1000;

            foreach (var prod in sampleProducts)
            {
                foreach (var color in defaultColors)
                {
                    foreach (var size in defaultSizes)
                    {
                        skuCounter++;
                        allVariants.Add(new ProductVariant
                        {
                            ProductID = prod.ProductID,
                            SKU = "NV-" + prod.ProductID + "-" + skuCounter,
                            ColorName = color,
                            SizeName = size,
                            StockQuantity = 15
                        });
                    }
                }
            }

            context.ProductVariants.AddRange(allVariants);
            context.SaveChanges();

            // 5. Tạo 1 đơn hàng mẫu để trang Admin và trang Lịch sử đơn hàng có sẵn dữ liệu demo
            var firstVariant = allVariants[0];
            var secondVariant = allVariants[5];

            var sampleOrder = new Order
            {
                UserID = customerUser.UserID,
                CustomerName = customerUser.FullName,
                CustomerPhone = customerUser.Phone,
                ShippingAddress = customerUser.Address,
                Note = "Giao giờ hành chính giúp mình",
                OrderStatus = "Completed",
                OrderDate = DateTime.Now.AddDays(-2),
                TotalAmount = (sampleProducts[0].EffectivePrice * 2) + sampleProducts[0].EffectivePrice
            };

            context.Orders.Add(sampleOrder);
            context.SaveChanges();

            context.OrderDetails.Add(new OrderDetail
            {
                OrderID = sampleOrder.OrderID,
                VariantID = firstVariant.VariantID,
                Quantity = 2,
                UnitPrice = sampleProducts[0].EffectivePrice
            });

            context.OrderDetails.Add(new OrderDetail
            {
                OrderID = sampleOrder.OrderID,
                VariantID = secondVariant.VariantID,
                Quantity = 1,
                UnitPrice = sampleProducts[0].EffectivePrice
            });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
