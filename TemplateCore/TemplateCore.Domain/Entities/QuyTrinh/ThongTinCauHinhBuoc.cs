namespace TemplateCore.Domain.Entities.QuyTrinh
{
    /// <summary>
    /// Table add add Node vào cấu hình
    /// </summary>
    public class ThongTinCauHinhBuoc: AuditableBaseEntity
    {
        [MaxLength(150)]
        public string? KeyNode {  get; set; }

        public int ThongTinCauHinhId {  get; set; }
    }
}
