using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using NovaShop.Models;

namespace NovaShop.Services
{
    public static class AuthService
    {
        private const string SessionUserKey = "User";

        public static string HashPassword(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                var sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static User GetCurrentUser()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
                return null;

            return HttpContext.Current.Session[SessionUserKey] as User;
        }

        public static bool IsAdmin()
        {
            var user = GetCurrentUser();
            return user != null && string.Equals(user.Role, "Admin", StringComparison.OrdinalIgnoreCase);
        }

        public static User Login(string email, string password, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Vui lòng nhập đầy đủ Email và Mật khẩu.";
                return null;
            }

            string cleanEmail = email.Trim().ToLowerInvariant();
            string passwordHash = HashPassword(password);

            using (var db = new ShopDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email.ToLower() == cleanEmail && u.PasswordHash == passwordHash);
                if (user == null)
                {
                    errorMessage = "Email hoặc mật khẩu không chính xác.";
                    return null;
                }

                if (!user.IsActive)
                {
                    errorMessage = "Tài khoản của bạn đang bị tạm khóa.";
                    return null;
                }

                HttpContext.Current.Session[SessionUserKey] = user;
                return user;
            }
        }

        public static User Register(string fullName, string email, string phone, string address, string password, out string errorMessage)
        {
            errorMessage = string.Empty;
            string cleanEmail = (email ?? string.Empty).Trim().ToLowerInvariant();

            using (var db = new ShopDbContext())
            {
                if (db.Users.Any(u => u.Email.ToLower() == cleanEmail))
                {
                    errorMessage = "Email này đã được đăng ký. Vui lòng sử dụng email khác.";
                    return null;
                }

                var newUser = new User
                {
                    FullName = fullName.Trim(),
                    Email = cleanEmail,
                    Phone = (phone ?? string.Empty).Trim(),
                    Address = (address ?? string.Empty).Trim(),
                    PasswordHash = HashPassword(password),
                    Role = "Customer",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                HttpContext.Current.Session[SessionUserKey] = newUser;
                return newUser;
            }
        }

        public static void Logout()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Remove(SessionUserKey);
            }
        }
    }
}
