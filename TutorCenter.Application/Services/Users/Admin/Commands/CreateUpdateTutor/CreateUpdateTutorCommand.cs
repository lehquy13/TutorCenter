using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users.Tutors;

namespace TutorCenter.Application.Services.Users.Admin.Commands.CreateUpdateTutor;

public record CreateUpdateTutorCommand
(
    TutorForDetailDto TutorForDetailDto,
    List<int> SubjectIds
) : IRequest<Result<bool>>;