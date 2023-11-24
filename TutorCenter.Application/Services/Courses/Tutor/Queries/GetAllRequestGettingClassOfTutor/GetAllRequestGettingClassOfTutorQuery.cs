using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;

namespace TutorCenter.Application.Services.Courses.Tutor.Queries.GetAllRequestGettingClassOfTutor;

public class GetAllRequestGettingClassOfTutorQuery : IRequest<Result<PaginatedList<RequestGettingClassForListDto>>>
{
    public GetAllRequestGettingClassOfTutorQuery()
    {
        PageIndex = 1;
    }

    public int ObjectId { get; set; }


    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 100;
}