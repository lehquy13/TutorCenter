using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Subjects;

namespace TutorCenter.Application.Services.Subjects.Queries;

public class GetSubjectQuery : IRequest<Result<SubjectDto>>
{
    public int Id { get; set; }
}