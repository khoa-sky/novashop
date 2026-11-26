-- =================================================================================
-- SCRIPT KHỞI TẠO CƠ SỞ DỮ LIỆU: NovaShopDB (SQL SERVER)
-- Dự án BTL ASP.NET Web Forms + Entity Framework 6 + Tailwind CSS
-- Lưu ý: Dự án đã có sẵn ShopDbInitializer tự động tạo DB khi chạy bằng EF6,
-- file SQL này dùng để chạy trực tiếp trong SSMS hoặc nộp kèm báo cáo BTL.
-- =================================================================================

USE master;
GO

IF DB_ID('NovaShopDB') IS NOT NULL
BEGIN
    ALTER DATABASE NovaShopDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE NovaShopDB;
END
GO

CREATE DATABASE NovaShopDB;
GO

USE NovaShopDB;
GO

-- 1. BẢNG USERS (Tài khoản Khách hàng & Admin)
CREATE TABLE dbo.Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    PasswordHash VARCHAR(128) NOT NULL,
    Phone VARCHAR(20) NULL,
    Address NVARCHAR(300) NULL,
    Role VARCHAR(20) NOT NULL DEFAULT 'Customer', -- 'Customer' | 'Admin'
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- 2. BẢNG CATEGORIES (Danh mục sản phẩm trên thanh Menu NOVA.VN)
CREATE TABLE dbo.Categories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    Slug VARCHAR(120) NULL,
    ParentID INT NULL REFERENCES dbo.Categories(CategoryID),
    DisplayOrder INT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1
);

-- 3. BẢNG PRODUCTS (Thông tin chung của mẫu quần áo)
CREATE TABLE dbo.Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryID INT NOT NULL REFERENCES dbo.Categories(CategoryID),
    ProductName NVARCHAR(200) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    SalePrice DECIMAL(18,2) NULL,
    Thumbnail VARCHAR(500) NULL,
    Description NVARCHAR(MAX) NULL,
    IsFeatured BIT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- 4. BẢNG PRODUCTVARIANTS (Biến thể Size / Màu / Tồn kho của từng mẫu quần áo)
CREATE TABLE dbo.ProductVariants (
    VariantID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL REFERENCES dbo.Products(ProductID) ON DELETE CASCADE,
    SKU VARCHAR(60) NULL,
    ColorName NVARCHAR(50) NOT NULL,
    SizeName VARCHAR(20) NOT NULL, -- 'S', 'M', 'L', 'XL'
    StockQuantity INT NOT NULL DEFAULT 0
);

-- 5. BẢNG PRODUCTIMAGES (Ảnh phụ của sản phẩm)
CREATE TABLE dbo.ProductImages (
    ImageID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL REFERENCES dbo.Products(ProductID) ON DELETE CASCADE,
    ImageUrl VARCHAR(500) NOT NULL,
    SortOrder INT NOT NULL DEFAULT 0
);

-- 6. BẢNG ORDERS (Đơn đặt hàng)
CREATE TABLE dbo.Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NULL REFERENCES dbo.Users(UserID),
    CustomerName NVARCHAR(100) NOT NULL,
    CustomerPhone VARCHAR(20) NOT NULL,
    ShippingAddress NVARCHAR(300) NOT NULL,
    Note NVARCHAR(500) NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    OrderStatus VARCHAR(30) NOT NULL DEFAULT 'Pending', -- 'Pending' | 'Shipping' | 'Completed' | 'Cancelled'
    OrderDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- 7. BẢNG ORDERDETAILS (Chi tiết từng món trong đơn hàng - gắn với VariantID)
CREATE TABLE dbo.OrderDetails (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL REFERENCES dbo.Orders(OrderID) ON DELETE CASCADE,
    VariantID INT NOT NULL REFERENCES dbo.ProductVariants(VariantID),
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL
);
GO

-- =================================================================================
-- DỮ LIỆU MẪU (SEED DATA)
-- Mật khẩu mặc định "123456" -> SHA-256: 8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92
-- =================================================================================

INSERT INTO dbo.Users (FullName, Email, PasswordHash, Phone, Address, Role, IsActive)
VALUES
(N'Quản Trị Viên NOVA', 'admin@nova.vn', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', '0909123456', N'770F Sư Vạn Hạnh, Quận 10, TP.HCM', 'Admin', 1),
(N'Nguyễn Văn Khách', 'khach@gmail.com', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', '0988777666', N'123 Cầu Giấy, Hà Nội', 'Customer', 1);

INSERT INTO dbo.Categories (CategoryName, Slug, DisplayOrder, IsActive)
VALUES
(N'ÁO THUN', 'ao-thun', 1, 1),
(N'ÁO SƠ MI', 'ao-so-mi', 2, 1),
(N'ÁO KHOÁC', 'ao-khoac', 3, 1),
(N'QUẦN JEANS', 'quan-jeans', 4, 1),
(N'QUẦN DÀI', 'quan-dai', 5, 1),
(N'QUẦN TÂY', 'quan-tay', 6, 1),
(N'QUẦN SHORT', 'quan-short', 7, 1),
(N'PHỤ KIỆN', 'phu-kien', 8, 1);
GO
