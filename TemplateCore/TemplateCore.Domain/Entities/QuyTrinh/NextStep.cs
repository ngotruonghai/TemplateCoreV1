namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class NextStep: AuditableBaseEntity
    {
        [MaxLength(150)]
        public string? NodeIdStart {  get; set; }

        [MaxLength(150)]
        public string? NodeIdEnd { get; set; }

        [MaxLength(150)]
        public string? DiagramId {  get; set; }

        [MaxLength(150)]
        public string? ActionName { get; set; }

        public int? Action { get; set; }

        public EnumStepQuyTrinh? TypeNextStep {  get; set; }

        public int DanhSachQuyTrinhId { get; set; }
    }
}
