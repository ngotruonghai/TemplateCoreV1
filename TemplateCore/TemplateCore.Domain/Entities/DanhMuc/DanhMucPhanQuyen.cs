namespace TemplateCore.Domain.Entities.DanhMuc
{
    public class DanhMucPhanQuyen: AuditableBaseEntity
    {
        public string UserId {  get; set; }

        public string DanhMucId { get; set; }
    }
}
