namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class DanhSachQuyTrinh: AuditableBaseEntity
    {
        [MaxLength(50)]
        public string MaQuyTrinh { get; set; }

        [MaxLength(350)]
        public string? TenQuyTrinh { get;set; }

        [MaxLength(100)]
        public string? UserId {  get; set; }

        [MaxLength(100)]
        public string UserName {  get; set; }

        public int DiagramNodeId {  get; set; }
        public ICollection<DiagramNode> DiagramNodes { get; set; }

        public int NodeId {  get; set; }
        public ICollection<Node> Nodes {  get; set; }
    }
}
