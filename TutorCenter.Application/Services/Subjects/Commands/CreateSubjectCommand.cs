using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Subjects;

namespace TutorCenter.Application.Services.Subjects.Commands;

public class CreateUpdateSubjectCommand
    : IRequest<Result<bool>>
{
    public SubjectDto SubjectDto { get; set; } = null!;
}

