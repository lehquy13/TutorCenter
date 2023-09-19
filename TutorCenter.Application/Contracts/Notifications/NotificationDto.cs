using TutorCenter.Application.Contracts.Models;
using TutorCenter.Domain.NotificationConsts;

namespace TutorCenter.Application.Contracts.Notifications;

public class NotificationDto : FullAuditedAggregateRootDto<int>
{
    public string Message { get; set; } = string.Empty;
    public int ObjectId { get; set; }
    public string DetailPath { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public NotificationEnum NotificationType { get; set; } = NotificationEnum.Unknown;
}