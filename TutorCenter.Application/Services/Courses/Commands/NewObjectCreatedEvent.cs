using MediatR;
using TutorCenter.Domain.NotificationConsts;

namespace TutorCenter.Application.Services.Courses.Commands;

internal record NewObjectCreatedEvent(int ObjectId, string Message, NotificationEnum NotificationEnum) : INotification;