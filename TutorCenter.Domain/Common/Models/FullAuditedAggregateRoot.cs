namespace TutorCenter.Domain.Common.Models;

public abstract class FullAuditedAggregateRoot<TId> : AuditedEntity<TId>
    where TId : notnull
{
    public bool IsDeleted { get; set; }
    public long? DeleterUserId { get; set; }
    public DateTime? DeletionTime { get; set; }
}