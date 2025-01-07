namespace TemplateCore.Domain.Entities
{
    public class KeyActive: AuditableBaseEntity
    {
        [MaxLength(150)]
        public string KeyACtiveCode { get; set; }

        /// <summary>
        /// Ngày bắt đầu kích hoạt
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Loại Key
        /// </summary>
        public EnumKeyActive TypeKey {  get; set; }

        public int Status { get; set; } = 1;
    }
}
