namespace TutorCenter.Domain.Interfaces.Services;
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}

