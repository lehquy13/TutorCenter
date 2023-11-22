namespace TutorCenter.Application.Contracts.Models;

public abstract class EntityDto<TId> : IEquatable<EntityDto<TId>>
    where TId : notnull
{
    protected EntityDto(TId id)
    {
        Id = id;
    }

#pragma warning disable CS8618

    protected EntityDto()
    {
    }

#pragma warning restore CS8618
    public TId Id { get; set; }

    public bool Equals(EntityDto<TId>? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        return obj is EntityDto<TId> entity && Id.Equals(entity.Id);
    }

    public static bool operator ==(EntityDto<TId>? left, EntityDto<TId>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EntityDto<TId>? left, EntityDto<TId>? right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}