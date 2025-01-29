namespace TemplateCore.Domain.Entities.QuyTrinh
{
    public class Node : AuditableBaseEntity
    {
        /// <summary>
        /// ID của Node
        /// </summary>
        [MaxLength(250)]
        public string KeyId { get; set; }

        public EnumStepQuyTrinh? Type { get; set; }

        [MaxLength(50)]
        public string? Index { get; set; }

        [MaxLength(150)]
        public string? TenNode { get; set; }

        public int DanhSachQuyTrinhId { get; set; }

        public int NodeSettingId {  get; set; }

        public virtual NodeSetting? NodeSettings { get; set; }
    }
}
