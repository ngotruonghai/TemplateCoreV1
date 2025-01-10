namespace TemplateCore.Domain.DTOs.QuyTrinh
{
    public class ListNodeModel
    {
        [MaxLength(250)]
        public string KeyId { get; set; }

        [MaxLength(100)]
        public string? type { get; set; }

        [MaxLength(50)]
        public string? position { get; set; }

        [MaxLength(150)]
        public string? label { get; set; }
    }
}
