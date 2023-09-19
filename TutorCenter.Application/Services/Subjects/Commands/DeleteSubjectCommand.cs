using FluentResults;
using MediatR;

namespace TutorCenter.Application.Services.Subjects.Commands;

public record DeleteSubjectCommand(
   int SubjectId
): IRequest<Result<bool>>;

