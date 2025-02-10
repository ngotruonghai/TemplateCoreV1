namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class ThongTinCauHinhBuoc: AuditableBaseEntity
    {
        [MaxLength(150)]
        public string? KeyNode {  get; set; }

        public int ThongTinCauHinhId {  get; set; }
    }
}
