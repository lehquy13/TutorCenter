using MediatR;

namespace TutorCenter.Application.Services.SubscribeRegistration.Commands;

public record EmailUnSubscriptionCommand(string Mail) : IRequest<bool>;
