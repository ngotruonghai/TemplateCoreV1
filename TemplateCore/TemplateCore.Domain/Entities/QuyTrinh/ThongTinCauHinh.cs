namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class ThongTinCauHinh: AuditableBaseEntity
    {
        public int Index { get; set; }

        [MaxLength(150)]
        public string? TenThonTin { get; set; }

        [MaxLength(350)]
        public string? NoiDung { get; set; }

        [MaxLength(350)]
        public string? ThongBao { get; set; }

        public EnumLoaiThongTin LoaiThongTin { get; set; }

        public int? KichThuocKyTu { get; set; }

        public bool IsBatBuocnhap {  get; set; }

        public bool IsFileDinhKem { get; set; }

        public int DanhSachQuyTrinhId {  get; set; }

        public virtual ICollection<ThongTinCauHinhBuoc> ThongTinCauHinhBuocs {  get; set; }
    }
}
