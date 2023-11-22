using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Courses.Dtos;

namespace TutorCenter.Application.Services.Courses.Commands;

public class CreateUpdateCourseCommand
    : IRequest<Result<bool>>
{
    public CourseForDetailDto CourseDto { get; set; } = null!;
    public string Email { get; set; } = null!;
}