using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Subjects;

namespace TutorCenter.Application.Services.Subjects.Queries;

public class GetAllSubjectsQuery : IRequest<Result<List<SubjectDto>>>
{
    /// <summary>
    ///     if(ObjectId == int.Empty) => GetAll
    /// </summary>
    public int ObjectId { get; set; } = 0;
}