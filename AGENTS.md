# 🤖 QUY TẮC BẮT BUỘC CHO AI AGENT TRONG DỰ ÁN NOVA.VN (`AGENTS.md`)

File `AGENTS.md` này là **Workspace Rule tự động kích hoạt (`Always-On Rule`)** cho mọi AI Agent (Antigravity, Cursor, GitHub Copilot, Claude Code) khi bất kỳ thành viên nào trong nhóm 4 người mở thư mục dự án `NovaShop` (hoặc mở `NovaShop.sln`).

---

## 1. BẮT BUỘC ĐỌC VÀ ÁP DỤNG CÁC SKILL TRONG `.agents/skills/`

Khi thành viên trong nhóm yêu cầu code, chỉnh sửa, thiết kế giao diện `.aspx` (Tailwind CSS) hoặc xử lý nghiệp vụ `.aspx.cs` (ASP.NET Web Forms + Entity Framework 6), AI Agent **BẮT BUỘC** phải đọc (`read_file`) các file `SKILL.md` sau trước khi viết code:

1. **Kiến trúc ASP.NET Web Forms + EF6 + Tailwind CSS của NovaShop (`aspnet-webforms-ef6-tailwind`):**
   - Đường dẫn: `.agents/skills/aspnet-webforms-ef6-tailwind/SKILL.md`
   - Áp dụng khi: Code tất cả các trang `.aspx` và `.aspx.cs`, sử dụng `<asp:Repeater>`, gọi tầng `NovaShop.Services` (`AuthService`, `CatalogService`, `CartService`, `OrderService`) và các thuộc tính tính toán sẵn (`EffectivePrice`, `HasSale`, `DiscountPercent`, `VariantDisplay`, `StatusVietnamese`).
2. **Thiết kế thẩm mỹ & Bố cục UI Thời trang (`frontend-design`):**
   - Đường dẫn: `.agents/skills/frontend-design/SKILL.md`
   - Áp dụng khi: Thiết kế giao diện Storefront (`Default.aspx`, `Products.aspx`, `ProductDetail.aspx`, `Cart.aspx`, `Checkout.aspx`, `Login.aspx`, `Register.aspx`, `MyOrders.aspx`) và trang quản trị (`Admin/*`) theo phong cách Editorial Minimalist của **NOVA.VN**.
3. **Tiêu chuẩn Trải nghiệm Người dùng & Giao diện Web (`web-design-guidelines`):**
   - Đường dẫn: `.agents/skills/web-design-guidelines/SKILL.md`
   - Áp dụng khi: Kiểm tra bố cục Responsive (Mobile/Desktop), trạng thái Hover/Focus, thông báo lỗi/thành công, độ tương phản màu sắc và khả năng tương tác của form đặt hàng/quản trị.

---

## 2. BẮT BUỘC TUÂN THỦ PHẠM VI FILE CỦA TỪNG THÀNH VIÊN (CHỐNG XUNG ĐỘT GIT)

Trước khi sửa bất kỳ file nào hoặc chạy lệnh `git commit`, AI Agent phải kiểm tra `docs/03_PHAN_CONG_VA_QUY_TAC_GIT.md` và `docs/04_HIEN_TRANG_DU_AN.md`:

- **Thành viên 1 — Team Lead (Nhánh `feature/lead-core-checkout`):**
  - Phụ trách: `Site.Master(.cs)`, `Admin/Admin.Master(.cs)`, `Cart.aspx(.cs)`, `Checkout.aspx(.cs)`, `Services/CartService.cs`, `Services/OrderService.cs`, `Models/*`, `Web.config`, `NovaShop.csproj`, `NovaShop.sln`.
- **Thành viên 2 — Storefront & Catalog Dev (Nhánh `feature/mem2-storefront-catalog`):**
  - Chỉ sửa: `Default.aspx(.cs)`, `Products.aspx(.cs)`, `ProductDetail.aspx(.cs)`, `Services/CatalogService.cs`, `Assets/css/custom.css`, `Assets/js/main.js`.
- **Thành viên 3 — Auth & Customer Portal Dev (Nhánh `feature/mem3-auth-myorders`):**
  - Chỉ sửa: `Login.aspx(.cs)`, `Register.aspx(.cs)`, `MyOrders.aspx(.cs)`, `Services/AuthService.cs`.
- **Thành viên 4 — Admin Panel & Dashboard Dev (Nhánh `feature/mem4-admin-panel`):**
  - Chỉ sửa: `Admin/Default.aspx(.cs)`, `Admin/Categories.aspx(.cs)`, `Admin/Products.aspx(.cs)`, `Admin/ProductVariants.aspx(.cs)`, `Admin/Orders.aspx(.cs)`.

> **QUY TẮC VÀNG:** Các thành viên 2, 3, 4 **TUYỆT ĐỐI KHÔNG** tự ý sửa `NovaShop.csproj`, `Web.config`, `Models/*`, `Site.Master` hoặc `Admin/Admin.Master` để đảm bảo 0% xung đột (Merge Conflict) khi gộp nhánh vào `master`.

