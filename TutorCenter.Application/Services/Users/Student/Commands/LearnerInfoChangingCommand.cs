using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users.Learners;

namespace TutorCenter.Application.Services.Users.Student.Commands;

public record LearnerInfoChangingCommand
(
    LearnerForUpdateDto LearnerDto,
    string? FilePath
    ) : IRequest<Result<bool>>;

