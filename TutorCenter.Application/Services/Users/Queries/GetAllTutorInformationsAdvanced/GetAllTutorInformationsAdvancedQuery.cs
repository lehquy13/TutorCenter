using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Domain.ClassInformationConsts;

namespace TutorCenter.Application.Services.Users.Queries.GetAllTutorInformationsAdvanced;

public class GetAllTutorInformationsAdvancedQuery : IRequest<Result<PaginatedList<TutorForListDto>>>
{
    public GetAllTutorInformationsAdvancedQuery()
    {
        PageIndex = 1;
    }

    public string SubjectName { get; set; } = string.Empty;
    public string TutorName { get; set; } = string.Empty;
    public int BirthYear { get; set; } = 0;
    public AcademicLevel Academic { get; set; } = AcademicLevel.Optional;
    public Gender Gender { get; set; } = Gender.None;
    public string Address { get; set; } = string.Empty;
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 100;
}