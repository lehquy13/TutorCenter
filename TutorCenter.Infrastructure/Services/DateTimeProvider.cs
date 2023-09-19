using TutorCenter.Domain.Interfaces.Services;

namespace TutorCenter.Infrastructure.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

