using TutorCenter.Domain.Common.Models;
using TutorCenter.Domain.NotificationConsts;

namespace TutorCenter.Domain.Notifications;

public class Notification : FullAuditedAggregateRoot<int>
{
    public string Message { get; set; } = string.Empty;
    public int ObjectId { get; set; }
    public bool IsRead { get; set; }
    public NotificationEnum NotificationType { get; set; } = NotificationEnum.Unknown;
}
