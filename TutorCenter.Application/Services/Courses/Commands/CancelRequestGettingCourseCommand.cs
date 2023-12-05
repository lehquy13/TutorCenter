using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Courses.Dtos;

namespace TutorCenter.Application.Services.Courses.Commands;

public record CancelRequestGettingCourseCommand(
    RequestGettingClassMinimalDto RequestGettingClassMinimalDto
) : IRequest<Result<bool>>;
