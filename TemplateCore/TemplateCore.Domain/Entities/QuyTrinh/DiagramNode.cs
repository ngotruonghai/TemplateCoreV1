namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class DiagramNode: AuditableBaseEntity
    {
        /// <summary>
        /// ID của Node
        /// </summary>
        [MaxLength(150)]
        public string KeyId { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? Source { get; set; }

        [MaxLength(150)]
        public string? Target { get; set; }

        [MaxLength(150)]
        public string? TenDiagram { get; set; } = string.Empty ;

        public int DanhSachQuyTrinhId { get; set; }
    }
}
