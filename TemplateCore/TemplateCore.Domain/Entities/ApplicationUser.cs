namespace TemplateCore.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(250)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(250)]
        public string LastName { get; set; } = string.Empty;
        public TypeAccount TypeUser { get; set; } = TypeAccount.UserTao;
        [MaxLength(250)]
        public string? ParentUserId { get; set; } = null;
        [MaxLength(150)]
        public string? KeyActiveId { get; set; } = null;
        [MaxLength(250)]
        public string MaNhanVien { get; set; } = string.Empty;
        [MaxLength(250)]
        public string MaPhongBan { get; set; } = string.Empty;
    }
}
