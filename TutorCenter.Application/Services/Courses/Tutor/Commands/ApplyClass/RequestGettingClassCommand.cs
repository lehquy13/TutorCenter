using FluentResults;
using MediatR;

namespace TutorCenter.Application.Services.Courses.Tutor.Commands.ApplyClass;

public record RequestGettingClassCommand
(
    int TutorId,
    int ClassId
) : IRequest<Result<bool>>;