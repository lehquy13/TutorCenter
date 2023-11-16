using FluentResults;
using MediatR;

namespace TutorCenter.Application.Services.Users.Admin.Commands.RemoveTutorReview;

public record RemoveTutorReviewCommand(int Guid) : IRequest<Result<bool>>;
