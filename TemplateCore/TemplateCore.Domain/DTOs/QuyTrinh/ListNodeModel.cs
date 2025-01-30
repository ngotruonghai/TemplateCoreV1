namespace TemplateCore.Domain.DTOs.QuyTrinh
{
    public class ListNodeModel
    {
        [MaxLength(250)]
        public string KeyId { get; set; }

        public EnumStepQuyTrinh? Type { get; set; }

        public int? Index { get; set; }

        [MaxLength(150)]
        public string? TenNode { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
