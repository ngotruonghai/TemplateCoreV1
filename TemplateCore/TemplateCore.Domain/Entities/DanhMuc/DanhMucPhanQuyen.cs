namespace TemplateCore.Domain.Entities.DanhMuc
{
    public class DanhMucPhanQuyen: AuditableBaseEntity
    {
        public string UserId {  get; set; }

        public int DanhMucId { get; set; }
    }
}
