using FluentResults;
using MediatR;

namespace TutorCenter.Application.Services.Users.Admin.Commands.RemoveTutorVerification;

public record RemoveTutorVerificationCommand(int Guid) : IRequest<Result<bool>>;
