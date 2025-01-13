using System.ComponentModel.DataAnnotations;

namespace TemplateCore.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(250)]
        public string FirstName { get; set; }
        [MaxLength(250)]
        public string LastName { get; set; }
        public TypeAccount TypeUser { get; set; } = TypeAccount.UserTao;
        [MaxLength(250)]
        public string? ParentUserId { get; set; } = null;
        [MaxLength(150)]
        public string? KeyActiveId { get; set; } = null;
        [MaxLength(250)]
        public string MaNhanVien { get; set; }
        [MaxLength(250)]
        public string? MaPhongBan { get; set; }
        public int? PhongBanId {  get; set; }
        [NotMapped]
        public string? RoleId { get; set; }
    }
}
