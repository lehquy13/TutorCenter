using TutorCenter.Application.Contracts.Notifications;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.DashBoard.Queries;

public class GetNotificationQuery : GetObjectQuery<List<NotificationDto>>
{
}