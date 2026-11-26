using System;
using System.Web.UI;

namespace NovaShop.Admin
{
    public partial class AdminMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // TODO (Team Lead): Kiểm tra quyền Admin (AuthService.IsAdmin()) trước khi cho vào trang quản trị
        }
    }
}
