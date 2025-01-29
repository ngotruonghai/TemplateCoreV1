namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class DanhSachQuyTrinh: AuditableBaseEntity
    {
        [MaxLength(50)]
        public string? MaQuyTrinh { get; set; }

        [MaxLength(10)]
        public string? ThietLapMaPhieu {  get; set; }

        [MaxLength(350)]
        public string? TenQuyTrinh { get;set; }

        [MaxLength(100)]
        public string? UserId {  get; set; }

        [MaxLength(100)]
        public string UserName {  get; set; }

        [MaxLength(500)]
        public string? GhiChu {  get; set; }

        /// <summary>
        /// Ngày bắt đầu quy trình
        /// </summary>
        public DateTime NgayBatDau { get; set; } = DateTime.Now;

        [MaxLength(150)]
        public string? NoiDung {  get; set; }

        public virtual ICollection<DiagramNode>? DiagramNodes { get; set; }

        public virtual ICollection<Node>? Nodes {  get; set; }

        public virtual ICollection<NhanSuTheoDoiQuyTrinh>? NhanSuTheoDoiQuyTrinhs { get; set; }

        public virtual ICollection<PhongBanTheoDoiQuyTrinh>? PhongBanTheoDoiQuyTrinhs { get; set; }

        public virtual ICollection<NextStep>? NextSteps { get; set; }
    }
}
