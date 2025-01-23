namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class ThongTinNoiDungQuyTrinh : AuditableBaseEntity
    {
        [MaxLength(150)]
        public string TenThongTin { get; set; }

        [MaxLength(150)]
        public string NoiDung { get; set; }

        /// <summary>
        /// VD: 1$2$3$4
        /// </summary>
        [MaxLength(350)]
        public string CauHinhHienThiBuoc { get; set; }

        public bool BatBuocNhap { get; set; } = true;

        public bool FileDinhKem { get; set; } = true;

        public virtual ICollection<NhanSuTheoDoiQuyTrinh>? NhanSuTheoDoiQuyTrinhs { get; set; }

    }
}
