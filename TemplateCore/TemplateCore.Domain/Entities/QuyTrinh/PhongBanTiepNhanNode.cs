namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class PhongBanTiepNhanNode: AuditableBaseEntity
    {
        public int PhongbanId { get; set; }

        [MaxLength(250)]
        public string? TenPhongBan { get; set; }

        public EnumCauHinhPhongBanQuyTrinh LoaiPhongBan { get; set; }

        public int? NodeSettingId { get; set; }
    }
}
