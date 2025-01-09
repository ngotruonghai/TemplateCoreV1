namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class DiagramNode: AuditableBaseEntity
    {
        /// <summary>
        /// ID của Node
        /// </summary>
        [MaxLength(250)]
        public string KeyId { get; set; }

        [MaxLength(100)]
        public string? source { get; set; }

        public string? lineAttributes { get; set; }

        public int DanhSachQuyTrinhId { get; set; }
        public virtual DanhSachQuyTrinh DanhSachQuyTrinhs { get; set; }
    }
}
