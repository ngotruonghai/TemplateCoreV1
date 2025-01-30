namespace TemplateCore.Domain.DTOs.QuyTrinh
{
    public class ListDiagramModel
    {
        [MaxLength(150)]
        public string KeyId { get; set; }

        [MaxLength(150)]
        public string? Source { get; set; }

        [MaxLength(150)]
        public string? Target { get; set; }

        [MaxLength(150)]
        public string? TenDiagram { get; set; }
    }
}
