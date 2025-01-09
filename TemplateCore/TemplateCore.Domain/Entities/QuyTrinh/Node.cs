namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class Node : AuditableBaseEntity
    {
        /// <summary>
        /// ID của Node
        /// </summary>
        [MaxLength(250)]
        public string KeyId { get; set; }

        [MaxLength(100)]
        public string? type { get; set; }

        [MaxLength(50)]
        public string? position { get; set; }

        [MaxLength(150)]
        public string? label { get; set; }

        public int DanhSachQuyTrinhId { get; set; }
        public virtual DanhSachQuyTrinh DanhSachQuyTrinhs { get; set; }
    }
}
