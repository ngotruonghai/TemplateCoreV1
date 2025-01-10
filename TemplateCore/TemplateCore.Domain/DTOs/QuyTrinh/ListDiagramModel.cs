namespace TemplateCore.Domain.DTOs.QuyTrinh
{
    public class ListDiagramModel
    {
        [MaxLength(250)]
        public string KeyId { get; set; }

        [MaxLength(100)]
        public string? source { get; set; }

        public string? target { get; set; }

        public string? lineAttributes { get; set; }
    }
}
