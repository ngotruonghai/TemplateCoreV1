namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class LichSuQuyTrinh: AuditableBaseEntity
    {

        public int DanhSachQuyTrinh {  get; set; }

        [MaxLength(350)]
        public string? LyDo {  get; set; }

        [MaxLength(350)]
        public string? GhiChu {  get; set; }
    }
}
