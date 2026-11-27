# 04. BÁO CÁO HIỆN TRẠNG DỰ ÁN — NOVA.VN (`NovaShop`)

> **Cập nhật lần cuối:** 27/11/2026  
> **Dự án:** Website Bán Quần Áo Thời Trang Hiện Đại — **NOVA.VN (`NovaShop`)**  
> **Công nghệ:** ASP.NET Web Forms (`.NET Framework 4.7.2`) + Entity Framework 6 (`EF 6.4.4`) + SQL Server + Tailwind CSS  
> **Repository GitHub (Public):** [https://github.com/khoa-sky/novashop](https://github.com/khoa-sky/novashop)  
> **Trạng thái tổng thể:** **Đã hoàn thành 100% Giai đoạn 1 → Giai đoạn 4 (Foundation, Database, EF6 Models, Core Business Services, Khung Solution & Phân nhánh Git).** Sẵn sàng cho 4 thành viên bắt đầu code song song **Giai đoạn 5 (Storefront/Customer UI)** và **Giai đoạn 6 (Admin Panel UI)**.

---

## 📊 1. Bảng Tổng Hợp Tiến Độ Theo Giai Đoạn (Roadmap Status)

| Giai đoạn | Tên giai đoạn | Trạng thái | Tỷ lệ | Chi tiết kết quả hiện tại |
| :---: | :--- | :---: | :---: | :--- |
| **GĐ 1** | Phân tích yêu cầu & Phạm vi | ✅ **Hoàn thành** | **100%** | Đã chốt 3 vai trò (`Guest`, `Customer`, `Admin`), ma trận chức năng MVP/Mở rộng, sơ đồ Use Case. |
| **GĐ 2** | Thiết kế CSDL & Entity Framework 6 | ✅ **Hoàn thành** | **100%** | Đã hoàn thiện 7 bảng chuẩn 3NF, `ShopDbContext`, `ShopDbInitializer` (20 sản phẩm + 240 biến thể Size/Màu) & script `InitShopDB.sql`. |
| **GĐ 3** | Thiết kế Khung Giao diện Tailwind CSS | ✅ **Hoàn thành** | **100%** | Đã tích hợp Tailwind CSS vào `Site.Master` và `Admin/Admin.Master`, cấu hình `custom.css` và `main.js`. |
| **GĐ 4** | Dựng Khung Codebase, Tầng Services & Git | ✅ **Hoàn thành** | **100%** | Đã viết xong **100% 4 lớp Service nghiệp vụ** (`AuthService`, `CatalogService`, `CartService`, `OrderService`), tạo đủ 13 trang `.aspx` khung và đẩy đủ 6 nhánh lên GitHub. |
| **GĐ 5** | Phát triển Giao diện & Code-behind Người dùng | ⏳ **Đang chờ thực hiện** | **15%** | Đã có sẵn khung file `.aspx` và toàn bộ hàm backend trong `Services/*`; cần gắn HTML Tailwind (`Repeater`, Form) và sự kiện `.aspx.cs`. |
| **GĐ 6** | Phát triển Giao diện & Code-behind Admin | ⏳ **Đang chờ thực hiện** | **15%** | Đã có sẵn `Admin.Master` và 5 trang `.aspx` khung trong `/Admin/*`; cần gắn bảng hiển thị, form CRUD và gọi EF6 / `OrderService`. |
| **GĐ 7** | Kiểm thử tích hợp (End-to-End Test) | ⚪ **Chưa bắt đầu** | **0%** | Thực hiện sau khi 4 thành viên merge nhánh `feature/*` vào `develop`. |
| **GĐ 8** | Hoàn thiện Báo cáo Word, Slide & Bảo vệ | ⚪ **Chưa bắt đầu** | **0%** | Đã có sẵn sơ đồ Mermaid (Use Case, ERD, Git Flow) trong `docs/` để copy trực tiếp vào báo cáo. |

---

## ✅ 2. Những Hạng Mục ĐÃ HOÀN THÀNH 100% (Sẵn sàng sử dụng ngay)

### 2.1. Hạ tầng Solution, Cấu hình & Kho Git (`100%`)
- **Solution & Project chuẩn thương hiệu mới:**
  - `NovaShop.sln` và `NovaShop.csproj` (`RootNamespace` & `AssemblyName` đều là `NovaShop`, đã xóa bỏ hoàn toàn tên cũ `YaMe`).
  - `Web.config`: Đã cấu hình sẵn `connectionStrings` tên `ShopDbContext` trỏ tới `(localdb)\MSSQLLocalDB` (`Initial Catalog=NovaShopDB`), cấu hình `EntityFramework 6.4.4`, `sessionState` (60 phút) và `globalization` (`vi-VN`, `utf-8`).
  - `Global.asax` & `Global.asax.cs`: Tự động chạy `Database.SetInitializer(new ShopDbInitializer())` và kích hoạt tạo DB ngay lần đầu ứng dụng khởi động.
- **Kho GitHub & 6 Nhánh làm việc (`https://github.com/khoa-sky/novashop`):**
  - Nhánh chính: `main` và `develop`.
  - 4 nhánh cá nhân đã tạo sẵn trên remote:
    1. `feature/lead-core-checkout` (Thành viên 1 - Team Lead)
    2. `feature/mem2-storefront-catalog` (Thành viên 2 - Storefront & Catalog)
    3. `feature/mem3-auth-myorders` (Thành viên 3 - Auth & Customer Portal)
    4. `feature/mem4-admin-panel` (Thành viên 4 - Admin Panel & Dashboard)

### 2.2. Tầng Dữ liệu & Entity Framework 6 (`Models/*` & `Database/*` — `100%`)
Toàn bộ các lớp trong thư mục `Models/` đã được code hoàn chỉnh, có đầy đủ Data Annotations, Navigation Properties và thuộc tính tính toán phụ trợ (Helper Properties) để bind lên giao diện `.aspx` cực dễ:

| File trong `Models/` / `Database/` | Trạng thái | Các thuộc tính & tiện ích đã code sẵn |
| :--- | :---: | :--- |
| `Models/ShopDbContext.cs` | ✅ **100%** | Khai báo đủ 7 `DbSet<>`: `Users`, `Categories`, `Products`, `ProductVariants`, `ProductImages`, `Orders`, `OrderDetails`. Cấu hình độ chính xác tiền tệ `decimal(18,2)`. |
| `Models/User.cs` | ✅ **100%** | Lưu thông tin tài khoản, `PasswordHash` (SHA-256), `Role` (`Customer` / `Admin`), `IsActive`, danh sách `Orders`. |
| `Models/Category.cs` | ✅ **100%** | Hỗ trợ danh mục đa cấp (`ParentID`), `Slug`, `DisplayOrder`, `IsActive`, danh sách `Products`. |
| `Models/Product.cs` | ✅ **100%** | Ngoài các cột DB, đã viết sẵn 3 thuộc tính `[NotMapped]` cực tiện cho UI:<br>- `HasSale`: Kiểm tra sản phẩm có đang giảm giá không.<br>- `EffectivePrice`: Giá bán thực tế (ưu tiên `SalePrice`).<br>- `DiscountPercent`: Tự tính `%` giảm giá (VD: `20%`) để hiển thị badge SALE. |
| `Models/ProductVariant.cs` | ✅ **100%** | Lưu `SKU` (`NV-...`), `ColorName`, `SizeName` (`S/M/L/XL`), `StockQuantity`. |
| `Models/ProductImage.cs` | ✅ **100%** | Lưu danh sách ảnh phụ của sản phẩm (`ImageUrl`, `SortOrder`). |
| `Models/Order.cs` | ✅ **100%** | Lưu thông tin đơn hàng; đã có sẵn thuộc tính `[NotMapped] StatusVietnamese` tự dịch trạng thái sang tiếng Việt (*Chờ xử lý*, *Đang giao hàng*, *Hoàn thành*, *Đã hủy*). |
| `Models/OrderDetail.cs` | ✅ **100%** | Chi tiết từng dòng sản phẩm trong đơn gắn với `VariantID`; có sẵn `[NotMapped] LineTotal` (`Quantity * UnitPrice`). |
| `Models/CartItem.cs` | ✅ **100%** | ViewModel lưu trong `Session["Cart"]`, có sẵn `VariantDisplay` (VD: `"Màu: Đen | Size: L"`), `LineTotal`, và `MaxStock`. |
| `Models/ShopDbInitializer.cs` | ✅ **100%** | Tự động Seed Data khi DB chưa tồn tại:<br>- **2 User:** `admin@nova.vn` / `123456` (Admin) và `khach@gmail.com` / `123456` (Customer).<br>- **8 Danh mục:** Áo thun, Áo sơ mi, Áo khoác, Quần Jeans, Quần dài, Quần tây, Quần short, Phụ kiện.<br>- **20 Sản phẩm mẫu** có ảnh Unsplash sắc nét + **240 Biến thể** (3 màu × 4 size/sản phẩm, tồn kho 15 cái/biến thể).<br>- **1 Đơn hàng mẫu** đã hoàn thành của tài khoản `khach@gmail.com`. |
| `Database/InitShopDB.sql` | ✅ **100%** | Script T-SQL độc lập tạo DB `NovaShopDB`, tạo 7 bảng và nạp Seed Data (dùng để nộp báo cáo hoặc chạy thủ công trên SSMS). |

### 2.3. Tầng Nghiệp vụ (`Services/*` — `100%`)
Điểm thuận lợi lớn nhất của codebase hiện tại là **toàn bộ logic truy vấn LINQ/EF6 khó nhất đã được viết sẵn 100% trong thư mục `Services/`**. Các thành viên khi làm trang `.aspx.cs` chỉ cần gọi hàm từ các class này mà không phải viết lại logic phức tạp:

| File trong `Services/` | Trạng thái | Các hàm nghiệp vụ đã viết sẵn cho các thành viên gọi |
| :--- | :---: | :--- |
| `Services/AuthService.cs` | ✅ **100%** | - `HashPassword(plainText)`: Băm mật khẩu chuẩn `SHA-256`.<br>- `GetCurrentUser()`: Lấy đối tượng `User` đang đăng nhập từ `Session["User"]`.<br>- `IsAdmin()`: Kiểm tra nhanh tài khoản hiện tại có phải Admin không.<br>- `Login(email, password, out errorMessage)`: Xác thực DB, kiểm tra khóa tài khoản và lưu Session.<br>- `Register(fullName, email, phone, address, password, out errorMessage)`: Kiểm tra trùng email, băm mật khẩu, tạo user mới và tự đăng nhập.<br>- `Logout()`: Xóa Session đăng nhập. |
| `Services/CatalogService.cs` | ✅ **100%** | - `GetActiveCategories()`: Lấy danh sách danh mục đang hoạt động theo thứ tự `DisplayOrder`.<br>- `GetFeaturedProducts(count)`: Lấy danh sách sản phẩm nổi bật cho Trang chủ.<br>- `GetLatestProducts(count)`: Lấy danh sách sản phẩm mới nhất.<br>- `SearchProducts(categoryId, keyword, onlySale, sortBy, page, pageSize, out totalCount)`: Hàm tổng hợp vừa lọc danh mục, lọc đồ SALE, tìm kiếm từ khóa, sắp xếp (`price_asc`, `price_desc`, `name_asc`, mới nhất) và phân trang (`Skip`/`Take`).<br>- `GetProductWithVariants(productId)`: Lấy chi tiết sản phẩm kèm luôn `.Include(Variants)` và `.Include(Images)`.<br>- `GetRelatedProducts(categoryId, excludeProductId, count)`: Lấy sản phẩm liên quan cùng danh mục. |
| `Services/CartService.cs` | ✅ **100%** | - `GetCart()`: Lấy danh sách `List<CartItem>` từ `Session["Cart"]`.<br>- `GetTotalQuantity()` & `GetTotalAmount()`: Tính tổng số lượng món và tổng tiền giỏ hàng.<br>- `AddToCart(variantId, quantity, out message)`: Kiểm tra tồn kho thực tế trong DB rồi thêm/cộng dồn vào giỏ hàng Session.<br>- `UpdateQuantity(variantId, newQuantity)`: Cập nhật số lượng (tự xóa nếu `<= 0`, chặn vượt `MaxStock`).<br>- `RemoveItem(variantId)` & `ClearCart()`. |
| `Services/OrderService.cs` | ✅ **100%** | - `CreateOrderFromCart(...)`: Dùng **EF6 Transaction (`BeginTransaction`)** kiểm tra tồn kho từng biến thể, lưu `Order` + `OrderDetails`, trừ `StockQuantity` và `Commit()` (hoặc `Rollback()` nếu hết hàng/lỗi).<br>- `GetOrdersByUser(userId)`: Lấy danh sách đơn hàng kèm chi tiết sản phẩm/biến thể của khách.<br>- `CancelOrderByCustomer(orderId, userId, out message)`: Khách hủy đơn khi đang `Pending` và **tự động hoàn lại tồn kho** cho từng biến thể.<br>- `UpdateOrderStatusByAdmin(orderId, newStatus, out message)`: Admin đổi trạng thái đơn (tự động hoàn kho nếu chuyển sang `Cancelled`, hoặc trừ lại kho nếu khôi phục từ `Cancelled`). |

---

## 🛠️ 3. Những Hạng Mục CẦN HOÀN THIỆN TIẾP (Giai đoạn 5 & 6 — Phần việc của 4 Thành viên)

Hiện tại, tất cả **2 Layout Master (`Site.Master`, `Admin.Master`)** và **13 trang `.aspx` + `.aspx.cs`** đều đang ở trạng thái **File Khung (Skeleton/Placeholder có đánh dấu `// TODO`)** để tránh xung đột file `.csproj`.

Dưới đây là bảng chi tiết hiện trạng và công việc cụ thể từng thành viên cần code thêm trong các file của mình:

### 👤 3.1. Thành viên 1 — Team Lead / Architect (Nhánh: `feature/lead-core-checkout`)

| File phụ trách | Hiện trạng | Công việc cụ thể cần code tiếp |
| :--- | :---: | :--- |
| `Site.Master` & `Site.Master.cs` | 🟡 **Đã có khung Header/Footer tĩnh** | - Thêm `<asp:Repeater ID="rptCategories">` trên thanh `<nav>` để render động 8 danh mục từ `CatalogService.GetActiveCategories()`.<br>- Hiển thị số lượng món trong giỏ hàng bằng `CartService.GetTotalQuantity()`.<br>- Kiểm tra `AuthService.GetCurrentUser()`: Nếu chưa đăng nhập hiện link *Đăng nhập / Đăng ký*; nếu đã đăng nhập hiện *Xin chào, [Tên]*, link *Đơn hàng của tôi*, link *Quản trị* (nếu `IsAdmin()`) và nút *Đăng xuất*. |
| `Admin/Admin.Master` & `Admin.Master.cs` | 🟡 **Đã có khung Sidebar tĩnh** | - Trong `Page_Init` của `Admin.Master.cs`: Kiểm tra `if (!AuthService.IsAdmin()) Response.Redirect("~/Login.aspx?returnUrl=" + ...);` để bảo vệ toàn bộ phân hệ Admin.<br>- Hiển thị tên Admin đang đăng nhập và nút Đăng xuất trên Sidebar. |
| `Cart.aspx` & `Cart.aspx.cs` | 🟡 **Khung rỗng (`TODO`)** | - Dùng `<asp:Repeater>` hiển thị danh sách món từ `CartService.GetCart()` (Ảnh, Tên SP, Màu/Size, Đơn giá, Ô nhập số lượng, Thành tiền, Nút xóa).<br>- Gọi `CartService.UpdateQuantity(...)`, `CartService.RemoveItem(...)` và nút chuyển sang `Checkout.aspx`. |
| `Checkout.aspx` & `Checkout.aspx.cs` | 🟡 **Khung rỗng (`TODO`)** | - Kiểm tra giỏ hàng trống thì đẩy về `Cart.aspx`. Nếu đã đăng nhập (`AuthService.GetCurrentUser()`), tự động điền sẵn Họ tên, SĐT, Địa chỉ vào form.<br>- Khi bấm **"Đặt hàng"**, gọi `OrderService.CreateOrderFromCart(...)`, nếu thành công thì gọi `CartService.ClearCart()` và chuyển sang `MyOrders.aspx` (hoặc hiện thông báo mã đơn hàng). |

---

### 👤 3.2. Thành viên 2 — Storefront & Catalog Dev (Nhánh: `feature/mem2-storefront-catalog`)

| File phụ trách | Hiện trạng | Công việc cụ thể cần code tiếp |
| :--- | :---: | :--- |
| `Default.aspx` & `Default.aspx.cs` | 🟡 **Khung rỗng (`TODO`)** | - Dựng giao diện Trang chủ Tailwind theo phong cách NOVA.VN: Khối Hero Editorial, 3 Banner chiến dịch.<br>- Thêm 2 `<asp:Repeater>` gọi `CatalogService.GetFeaturedProducts(8)` (Sản phẩm nổi bật) và `CatalogService.GetLatestProducts(8)` (Sản phẩm mới). Hiển thị badge `SALE -xx%` dựa vào `HasSale` và `DiscountPercent`. |
| `Products.aspx` & `Products.aspx.cs` | 🟡 **Khung rỗng (`TODO`)** | - Tạo thanh bộ lọc: Danh mục (`?cat=`), Chỉ xem đồ giảm giá (`?sale=1`), Ô tìm kiếm từ khóa (`?kw=`), Dropdown sắp xếp (`price_asc`, `price_desc`, `name_asc`).<br>- Gọi `CatalogService.SearchProducts(...)` bind vào `<asp:Repeater>` lưới 4 cột và tạo thanh phân trang (`?page=1, 2...`). |
| `ProductDetail.aspx` & `ProductDetail.aspx.cs` | 🟡 **Khung rỗng (`TODO`)** | - Đọc `?id=` từ QueryString, gọi `CatalogService.GetProductWithVariants(productId)`.<br>- Hiển thị ảnh chính, ảnh phụ, tên, giá gốc/giá sale, mô tả.<br>- Cho người dùng chọn Biến thể (**Màu sắc + Size `S/M/L/XL`** - có thể dùng `DropDownList` hoặc nút chọn `VariantID`), nhập số lượng và bấm **"Thêm vào giỏ hàng"** → gọi `CartService.AddToCart(variantId, qty, out msg)`.<br>- Cuối trang hiển thị 4 sản phẩm liên quan bằng `CatalogService.GetRelatedProducts(...)`. |

---

### 👤 3.3. Thành viên 3 — Auth & Customer Portal Dev (Nhánh: `feature/mem3-auth-myorders`)

| File phụ trách | Hiện trạng | Công việc cụ thể cần code tiếp |
| :--- | :---: | :--- |
| `Login.aspx` & `Login.aspx.cs` | 🟡 **Khung rỗng (`TODO`)** | - Dựng Form Đăng nhập Tailwind (Email, Mật khẩu, Thông báo lỗi, Link sang `Register.aspx`).<br>- Khi bấm Đăng nhập: Gọi `AuthService.Login(email, pass, out error)`. Nếu thành công: kiểm tra `Request.QueryString["returnUrl"]`, hoặc nếu `AuthService.IsAdmin()` thì chuyển vào `~/Admin/Default.aspx`, ngược lại về `~/Default.aspx`. |
| `Register.aspx` & `Register.aspx.cs` | 🟡 **Khung rỗng (`TODO`)** | - Dựng Form Đăng ký Tailwind (Họ tên, Email, SĐT, Địa chỉ, Mật khẩu, Nhập lại mật khẩu).<br>- Kiểm tra mật khẩu khớp nhau, gọi `AuthService.Register(...)` và chuyển hướng sau khi đăng ký thành công. |
| `MyOrders.aspx` & `MyOrders.aspx.cs` | 🟡 **Khung rỗng (`TODO`)** | - Yêu cầu đăng nhập (nếu `AuthService.GetCurrentUser() == null` thì chuyển sang `Login.aspx?returnUrl=MyOrders.aspx`).<br>- Gọi `OrderService.GetOrdersByUser(user.UserID)` để hiển thị danh sách các đơn hàng đã đặt kèm chi tiết từng món (Tên áo/quần, Màu, Size, Số lượng, Đơn giá) và trạng thái tiếng Việt (`StatusVietnamese`).<br>- Thêm nút **"Hủy đơn hàng"** cho các đơn đang ở trạng thái `Pending` → gọi `OrderService.CancelOrderByCustomer(orderId, user.UserID, out msg)`. |

---

### 👤 3.4. Thành viên 4 — Admin Panel & Dashboard Dev (Nhánh: `feature/mem4-admin-panel`)

| File phụ trách | Hiện trạng | Công việc cụ thể cần code tiếp |
| :--- | :---: | :--- |
| `Admin/Default.aspx` & `.cs` | 🟡 **Khung rỗng (`TODO`)** | - Dùng `ShopDbContext` thống kê 4 thẻ KPI: Tổng doanh thu (các đơn `Completed`), Tổng số đơn hàng, Số đơn đang chờ xử lý (`Pending`), Tổng số sản phẩm đang bán.<br>- Hiển thị bảng cảnh báo các biến thể sắp hết hàng (`ProductVariants` có `StockQuantity < 5`) và 5 đơn hàng mới nhất. |
| `Admin/Categories.aspx` & `.cs` | 🟡 **Khung rỗng (`TODO`)** | - Bảng danh sách Danh mục + Form Thêm/Sửa/Ẩn-Hiện (`CategoryName`, `Slug`, `DisplayOrder`, `IsActive`) bằng `ShopDbContext`. |
| `Admin/Products.aspx` & `.cs` | 🟡 **Khung rỗng (`TODO`)** | - Bảng danh sách Sản phẩm (có lọc theo danh mục) + Form Thêm/Sửa/Ngừng bán sản phẩm (`ProductName`, `CategoryID`, `Price`, `SalePrice`, `Description`, `IsFeatured`, `IsActive`).<br>- Hỗ trợ nhập URL ảnh hoặc upload file ảnh qua `<asp:FileUpload>` lưu vào `~/Assets/images/products/`. |
| `Admin/ProductVariants.aspx` & `.cs` | 🟡 **Khung rỗng (`TODO`)** | - Chọn sản phẩm (hoặc truyền `?productId=`), hiển thị danh sách các biến thể (`SKU`, `ColorName`, `SizeName`, `StockQuantity`).<br>- Form Thêm biến thể mới (Màu + Size `S/M/L/XL` + Tồn kho) và cập nhật nhanh số lượng tồn kho. |
| `Admin/Orders.aspx` & `.cs` | 🟡 **Khung rỗng (`TODO`)** | - Bảng danh sách toàn bộ đơn hàng (có bộ lọc theo trạng thái `Pending`, `Shipping`, `Completed`, `Cancelled`), xem chi tiết các món trong đơn.<br>- Nút/Dropdown cập nhật trạng thái đơn hàng gọi trực tiếp hàm có sẵn `OrderService.UpdateOrderStatusByAdmin(orderId, newStatus, out msg)`. |

---

## 📌 4. Kết Luận & Hành Động Tiếp Theo Cho Nhóm

1. **Phần nền tảng khó nhất đã xong 100%:** Toàn bộ CSDL 7 bảng, Seed Data 20 sản phẩm + 240 biến thể, và cả 4 class `AuthService`, `CatalogService`, `CartService`, `OrderService` đã hoàn thiện và đẩy lên GitHub.
2. **Không lo xung đột Git:** Toàn bộ các file `.aspx` đã được khai báo sẵn trong `NovaShop.csproj`. Các thành viên tuyệt đối không cần thêm/xóa file trong Solution.
3. **Bước tiếp theo ngay bây giờ:**
   - 4 thành viên clone kho [https://github.com/khoa-sky/novashop](https://github.com/khoa-sky/novashop) về máy.
   - Mỗi người `git checkout` sang đúng nhánh `feature/...` của mình và hoàn thiện giao diện `.aspx` + gọi hàm Service trong `.aspx.cs` theo đúng bảng ở **Mục 3** trên đây.

