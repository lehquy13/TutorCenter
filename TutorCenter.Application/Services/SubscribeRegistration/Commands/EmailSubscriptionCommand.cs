using MediatR;

namespace TutorCenter.Application.Services.SubscribeRegistration.Commands;

public record EmailSubscriptionCommand(string Mail) : IRequest<bool>;
