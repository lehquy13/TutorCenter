namespace TutorCenter.Application.Contracts.Models;

public abstract class BasicAuditedEntityDto<TId> : EntityDto<TId>
    where TId : notnull
{
    protected BasicAuditedEntityDto(TId id) : base(id)
    {
    }

    protected BasicAuditedEntityDto()
    {
        LastModificationTime = DateTime.Now;
        CreationTime = DateTime.Now;
    }

    public DateTime? LastModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
}