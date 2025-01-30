namespace TemplateCore.Domain.DTOs.QuyTrinh
{
    public class NextStepModel
    {
        [MaxLength(150)]
        public string? NodeIdStart { get; set; }

        [MaxLength(150)]
        public string? NodeIdEnd { get; set; }

        [MaxLength(150)]
        public string? DiagramId { get; set; }

        [MaxLength(150)]
        public string? ActionName { get; set; }

        public int? Action { get; set; }

        public EnumStepQuyTrinh? TypeNextStep { get; set; }
    }
}
