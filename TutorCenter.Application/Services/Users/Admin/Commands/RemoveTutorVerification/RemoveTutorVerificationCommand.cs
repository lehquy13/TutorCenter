using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public record RemoveTutorVerificationCommand(int Guid) : IRequest<Result<bool>>;
