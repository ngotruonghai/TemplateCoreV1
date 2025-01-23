namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class PhongBanTheoDoiQuyTrinh: AuditableBaseEntity
    {
        [MaxLength(100)]
        public string PhongbanId { get; set; }

        [MaxLength(250)]
        public string? TenPhongBan { get; set; }

        public EnumCauHinhPhongBanQuyTrinh LoaiPhongBan { get; set; }

        public int DanhSachQuyTrinhId { get; set; }
    }
}
