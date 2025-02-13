namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class Node : AuditableBaseEntity
    {
        [MaxLength(250)]
        public string KeyId { get; set; } = string.Empty;

        public EnumStepQuyTrinh? Type { get; set; }

        public int? Index { get; set; }

        [MaxLength(150)]
        public string? TenNode { get; set; }

        public int? X { get; set; }

        public int? Y { get; set; }

        public int DanhSachQuyTrinhId { get; set; }

        public virtual ICollection<NodeSetting>? NodeSettings { get; set; }
    }
}
