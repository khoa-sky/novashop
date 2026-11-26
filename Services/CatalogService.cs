using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NovaShop.Models;

namespace NovaShop.Services
{
    public static class CatalogService
    {
        public static List<Category> GetActiveCategories()
        {
            using (var db = new ShopDbContext())
            {
                return db.Categories
                         .Where(c => c.IsActive)
                         .OrderBy(c => c.DisplayOrder)
                         .ThenBy(c => c.CategoryName)
                         .ToList();
            }
        }

        public static List<Product> GetFeaturedProducts(int count = 8)
        {
            using (var db = new ShopDbContext())
            {
                return db.Products
                         .Include(p => p.Category)
                         .Where(p => p.IsActive && p.IsFeatured)
                         .OrderByDescending(p => p.CreatedAt)
                         .Take(count)
                         .ToList();
            }
        }

        public static List<Product> GetLatestProducts(int count = 8)
        {
            using (var db = new ShopDbContext())
            {
                return db.Products
                         .Include(p => p.Category)
                         .Where(p => p.IsActive)
                         .OrderByDescending(p => p.CreatedAt)
                         .Take(count)
                         .ToList();
            }
        }

        public static List<Product> SearchProducts(int? categoryId, string keyword, bool onlySale, string sortBy, int page, int pageSize, out int totalCount)
        {
            using (var db = new ShopDbContext())
            {
                var query = db.Products
                              .Include(p => p.Category)
                              .Where(p => p.IsActive);

                if (categoryId.HasValue && categoryId.Value > 0)
                {
                    query = query.Where(p => p.CategoryID == categoryId.Value);
                }

                if (onlySale)
                {
                    query = query.Where(p => p.SalePrice.HasValue && p.SalePrice.Value > 0 && p.SalePrice.Value < p.Price);
                }

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    string kw = keyword.Trim();
                    query = query.Where(p => p.ProductName.Contains(kw) || p.Description.Contains(kw));
                }

                totalCount = query.Count();

                switch (sortBy)
                {
                    case "price_asc":
                        query = query.OrderBy(p => p.SalePrice.HasValue && p.SalePrice.Value > 0 ? p.SalePrice.Value : p.Price);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(p => p.SalePrice.HasValue && p.SalePrice.Value > 0 ? p.SalePrice.Value : p.Price);
                        break;
                    case "name_asc":
                        query = query.OrderBy(p => p.ProductName);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.CreatedAt);
                        break;
                }

                int skip = Math.Max(0, (page - 1) * pageSize);
                return query.Skip(skip).Take(pageSize).ToList();
            }
        }

        public static Product GetProductWithVariants(int productId)
        {
            using (var db = new ShopDbContext())
            {
                return db.Products
                         .Include(p => p.Category)
                         .Include(p => p.Variants)
                         .Include(p => p.Images)
                         .FirstOrDefault(p => p.ProductID == productId && p.IsActive);
            }
        }

        public static List<Product> GetRelatedProducts(int categoryId, int excludeProductId, int count = 4)
        {
            using (var db = new ShopDbContext())
            {
                return db.Products
                         .Include(p => p.Category)
                         .Where(p => p.IsActive && p.CategoryID == categoryId && p.ProductID != excludeProductId)
                         .OrderByDescending(p => p.CreatedAt)
                         .Take(count)
                         .ToList();
            }
        }
    }
}
