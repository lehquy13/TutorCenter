using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Courses.Dtos;

namespace TutorCenter.Application.Services.Courses.Queries.GetCourseQuery;

public class GetCourseQuery : IRequest<Result<CourseForDetailDto>>
{
    public int Id { get; set; }
}