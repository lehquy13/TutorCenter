using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Courses.Dtos;

namespace TutorCenter.Application.Services.Courses.Queries.GetCoureRequestById;

public class GetCourseRequestByIdQuery : IRequest<Result<CourseRequestDto>>
{
    public int Id { get; set; }
}