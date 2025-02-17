namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class ThongTinNoiDungQuyTrinh : AuditableBaseEntity
    {
        [MaxLength(150)]
        public string TenThongTin { get; set; } = string.Empty;

        [MaxLength(150)]
        public string NoiDung { get; set; } = string.Empty;

        /// <summary>
        /// VD: 1$2$3$4
        /// </summary>
        [MaxLength(350)]
        public string CauHinhHienThiBuoc { get; set; } = string.Empty;

        public bool BatBuocNhap { get; set; } = true;

        public bool FileDinhKem { get; set; } = true;

        [MaxLength(500)]
        public string GhiChu {  get; set; } = string.Empty;

        public virtual ICollection<NhanSuTheoDoiQuyTrinh>? NhanSuTheoDoiQuyTrinhs { get; set; }

    }
}
