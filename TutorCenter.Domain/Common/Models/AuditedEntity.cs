namespace TutorCenter.Domain.Common.Models;

public abstract class AuditedEntity<TId> : Entity<TId>
    where TId : notnull
{
  
    public DateTime? LastModificationTime { get; set; }
    public long? LastModifierUserId { get; set; }
    public  DateTime CreationTime { get; set; }
    public  long? CreatorUserId { get; set; }

    protected AuditedEntity(TId id) : base(id)
    {
    }

    protected AuditedEntity()
    {
        LastModificationTime = DateTime.Now;
        CreationTime = DateTime.Now;
    }
}