---
name: aspnet-webforms-ef6-tailwind
description: Best practices, architecture patterns, and UI guidelines for building NOVA.VN (NovaShop) with ASP.NET Web Forms (.NET Framework 4.7.2), Entity Framework 6 (Code First / LINQ), and Tailwind CSS. Use whenever writing or modifying .aspx markup, .aspx.cs code-behind, MasterPages, or Admin pages in NovaShop.
---

# ASP.NET Web Forms + Entity Framework 6 + Tailwind CSS (NovaShop Skill)

This skill provides strict implementation patterns for the **NOVA.VN (`NovaShop`)** e-commerce project so all 4 team members produce clean, responsive, conflict-free code.

---

## 1. Visual Design System (NOVA.VN Editorial Minimalist Fashion)

- **Color Palette**:
  - Primary Text / CTA Background: `neutral-900` (`#171717`) / `black` (`#000000`)
  - Secondary / Muted Text: `neutral-500` (`#737373`) / `neutral-600`
  - Borders & Dividers: `neutral-200` (`#e5e5e5`)
  - Subtle Backgrounds: `neutral-50` (`#fafafa`) / `neutral-100` (`#f5f5f5`)
  - Sale / Discount Accent: `red-600` (`#dc2626`)
- **Typography**:
  - Editorial Headings (Hero, Section Titles): `font-serif` (`Playfair Display`) with `tracking-wide`
  - UI, Navigation, Prices, Body: `font-sans` (`Inter`) with uppercase tracking on labels (`text-xs uppercase tracking-wider font-medium`)
- **Component Classes**:
  - **Primary Button**: `bg-neutral-900 text-white hover:bg-neutral-800 px-6 py-3 text-xs font-semibold uppercase tracking-widest transition duration-200`
  - **Secondary / Outline Button**: `border border-neutral-900 text-neutral-900 hover:bg-neutral-900 hover:text-white px-6 py-3 text-xs font-semibold uppercase tracking-widest transition duration-200`
  - **Input / Select**: `w-full border border-neutral-300 px-3.5 py-2.5 text-sm focus:outline-none focus:border-neutral-900 transition`
  - **Product Card Grid**: `grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6`
  - **Product Thumbnail**: `aspect-[3/4] w-full object-cover bg-neutral-100 group-hover:scale-105 transition-transform duration-500`

---

## 2. ASP.NET Web Forms + Tailwind CSS Integration Rules

1. **Prefer `<asp:Repeater>` over `<asp:GridView>` on Storefront Pages**:
   - `<asp:GridView>` generates legacy HTML `<table>` tags with inline styles that break Tailwind grid layouts.
   - Always use `<asp:Repeater>` for product grids, category menus, cart items, variant buttons, and order history cards.
2. **Use `CssClass` on Server Controls**:
   - On `<asp:TextBox>`, `<asp:DropDownList>`, `<asp:Button>`, `<asp:LinkButton>`, pass Tailwind utilities via `CssClass="..."`.
3. **Avoid Duplicate `<form runat="server">`**:
   - Both `Site.Master` and `Admin/Admin.Master` already wrap `MainContent` inside `<form runat="server">`.
   - **NEVER** nest another `<form>` tag inside `.aspx` content pages.
4. **Always Check `!IsPostBack` in `Page_Load`**:
   - Wrap initial data binding in `if (!IsPostBack)` so Button clicks (`OnClick`) and Repeater commands (`OnItemCommand`) don't overwrite user input or reset dropdown selections.

```csharp
protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
    {
        BindData();
    }
}
```

---

## 3. Calling the Pre-Built `NovaShop.Services` Layer

Do **NOT** rewrite complex logic in `.aspx.cs` when a method already exists in `Services/`:

- **Authentication (`NovaShop.Services.AuthService`)**:
  - `AuthService.Login(email, password, out string error)`
  - `AuthService.Register(fullName, email, phone, address, password, out string error)`
  - `AuthService.GetCurrentUser()` / `AuthService.IsAdmin()` / `AuthService.Logout()`
- **Catalog & Products (`NovaShop.Services.CatalogService`)**:
  - `CatalogService.GetActiveCategories()`
  - `CatalogService.GetFeaturedProducts(8)` / `CatalogService.GetLatestProducts(8)`
  - `CatalogService.SearchProducts(categoryId, keyword, onlySale, sortBy, page, pageSize, out int totalCount)`
  - `CatalogService.GetProductWithVariants(productId)`
  - `CatalogService.GetRelatedProducts(categoryId, excludeProductId, 4)`
- **Shopping Cart (`NovaShop.Services.CartService`)**:
  - `CartService.GetCart()` / `CartService.GetTotalQuantity()` / `CartService.GetTotalAmount()`
  - `CartService.AddToCart(variantId, quantity, out string message)`
  - `CartService.UpdateQuantity(variantId, newQty)` / `CartService.RemoveItem(variantId)` / `CartService.ClearCart()`
- **Orders & Checkout (`NovaShop.Services.OrderService`)**:
  - `OrderService.CreateOrderFromCart(userId, name, phone, address, note, cartItems, out string error)` (Runs inside an EF6 Database Transaction and decrements `ProductVariant.StockQuantity`)
  - `OrderService.GetOrdersByUser(userId)`
  - `OrderService.CancelOrderByCustomer(orderId, userId, out string message)` (Restores stock automatically)
  - `OrderService.UpdateOrderStatusByAdmin(orderId, newStatus, out string message)`

---

## 4. Helpful Calculated Properties on EF6 Models

Use these `[NotMapped]` helper properties directly in `.aspx` data-binding expressions (`<%# Eval(...) %>`):

- **`Product`**:
  - `<%# Eval("EffectivePrice", "{0:N0}₫") %>`: Final price (`SalePrice` if on sale, otherwise `Price`).
  - `<%# (bool)Eval("HasSale") %>`: `true` when product has a valid `SalePrice < Price`.
  - `<%# Eval("DiscountPercent") %>`: Integer discount percentage (e.g., `20` for `-20%`).
- **`CartItem`**:
  - `<%# Eval("VariantDisplay") %>`: Formatted string `"Màu: Đen | Size: L"`.
  - `<%# Eval("LineTotal", "{0:N0}₫") %>`: `UnitPrice * Quantity`.
- **`Order`**:
  - `<%# Eval("StatusVietnamese") %>`: Vietnamese status label (*Chờ xử lý*, *Đang giao hàng*, *Hoàn thành*, *Đã hủy*).
