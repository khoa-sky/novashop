using System;
using System.Data.Entity;
using System.Web;
using NovaShop.Models;

namespace NovaShop
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Tự động khởi tạo CSDL và nạp dữ liệu mẫu (20+ sản phẩm thời trang NOVA) nếu DB chưa tồn tại
            Database.SetInitializer(new ShopDbInitializer());
        }
    }
}
