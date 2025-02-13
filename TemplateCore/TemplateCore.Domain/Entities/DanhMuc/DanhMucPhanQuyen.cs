namespace TemplateCore.Domain.Entities.DanhMuc
{
    public class DanhMucPhanQuyen: AuditableBaseEntity
    {
        public string UserId {  get; set; } = string.Empty;

        public int DanhMucId { get; set; }
    }
}
