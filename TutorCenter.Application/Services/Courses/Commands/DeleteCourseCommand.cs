using FluentResults;
using MediatR;

namespace TutorCenter.Application.Services.Courses.Commands;

public record DeleteCourseCommand
(
    int GuidId
) : IRequest<Result<bool>>;