using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Courses.Dtos;

namespace TutorCenter.Application.Services.Courses.Tutor.Queries.GetTeachingClassDetailQuery;

public class GetTeachingClassDetailQuery : IRequest<Result<RequestGettingClassExtendDto>>
{
    public int CourseId { get; set; }
    public int ObjectId { get; set; }
}