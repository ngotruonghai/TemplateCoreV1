namespace TemplateCore.Domain.Entities.PhongBan
{
    public class PhongBan: AuditableBaseEntity
    {
        [MaxLength(50)]
        public string MaPhongBan {  get; set; } = string.Empty;

        [MaxLength(150)]
        public string? TenPhongBan { get; set; }

        [MaxLength(350)]
        public string? GhiChu {  get; set; }
    }
}
