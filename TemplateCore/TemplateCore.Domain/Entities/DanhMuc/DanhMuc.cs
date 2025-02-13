namespace TemplateCore.Domain.Entities.DanhMuc
{
    public class DanhMuc : AuditableBaseEntity
    {
        [MaxLength(150)]
        public string TenDanhMuc { get; set; } = string.Empty;

        [MaxLength(50)]
        public string UrlLink { get; set; } = string.Empty;

        [MaxLength(350)]
        public string? GhiChu {  get; set; }

        public virtual ICollection<DanhMucPhanQuyen>? DanhMucPhanQuyens { get; set; }
    }
}
