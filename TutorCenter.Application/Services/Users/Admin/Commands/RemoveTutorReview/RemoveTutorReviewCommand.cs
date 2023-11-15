using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public record RemoveTutorReviewCommand(int Guid) : IRequest<Result<bool>>;
