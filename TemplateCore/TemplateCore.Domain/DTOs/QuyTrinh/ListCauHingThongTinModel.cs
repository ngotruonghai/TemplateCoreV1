namespace TemplateCore.Domain.DTOs.QuyTrinh
{
    public class ListCauHingThongTinModel
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

        public bool IsBatBuocnhap { get; set; } = false;

        public bool IsFileDinhKem { get; set; } = false;

        public string[]? DanhSachCauHinhBuoc {  get; set; }
    }
}
