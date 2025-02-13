namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class NhanSuTiepNhanNode: AuditableBaseEntity
    {
        [MaxLength(100)]
        public string UserId { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? HoTen { get; set; }

        public EnumCauHinhNhanSuQuyTrinh LoaiNhanSu { get; set; }

        public int? NodeSettingId { get; set; }
    }
}
