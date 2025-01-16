namespace TemplateCore.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public virtual int Id { get; set; }

        [MaxLength(150)]
        public string? CreatedBy { get; set; }

        public DateTime Created { get; set; }

        [MaxLength(150)]
        public string? LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public bool Status {  get; set; } = true;

        [MaxLength(100)]
        public string? UserParentId {  get; set; }
    }
}
