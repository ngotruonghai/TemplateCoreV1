namespace TemplateCore.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TypeAccount TypeUser { get; set; } = TypeAccount.UserTao;
        public string? ParentUser { get; set; } = null;
        public string? KeyActiveId { get; set; } = null;
        [NotMapped]
        public string MaNhanVien { get; set; }
    }
}
