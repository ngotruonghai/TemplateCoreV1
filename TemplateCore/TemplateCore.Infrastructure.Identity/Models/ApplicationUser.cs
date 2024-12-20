using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TemplateCore.Domain.DTOs.Account;

namespace TemplateCore.Infrastructure.Identity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string? MaNhanVien { get; set; }
        public string RoleId { get; set; }
        public string? TenNhanVien { get; set; }
        public string? ChucVu { get; set; }
        public string? MaDonVi { get; set; }
        public string? TenDonVi { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}
