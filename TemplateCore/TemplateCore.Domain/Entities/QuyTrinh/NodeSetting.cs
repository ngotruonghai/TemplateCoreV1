namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class NodeSetting: AuditableBaseEntity
    {
        [MaxLength(350)]
        public string? GhiChu {  get; set; }

        public int? NoodeId { get; set; }

        /// <summary>
        /// Số phút nhắc nhở qua mail
        /// </summary>
        public int? CauHinhMailNhacNho {  get; set; }

        public bool IsTaoTaskBaoCao { get; set; } = false;

        public bool IsGuiMailPhongBanTiepNhan {  get; set; } = false ;

        public bool IsGuiMailNhacNho { get; set; } = false;

        public virtual ICollection<NhanSuTiepNhanNode>? NhanSuTiepNhanNodes { get; set; }
        public virtual ICollection<PhongBanTiepNhanNode>? PhongBanTiepNhanNodes { get; set; }
    }
}
