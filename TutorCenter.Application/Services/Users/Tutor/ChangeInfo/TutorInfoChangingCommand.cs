using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users.Tutors;

namespace TutorCenter.Application.Services.Users.Tutor.ChangeInfo;

public record TutorInfoChangingCommand
(
    TutorBasicDto TutorDto,
    List<int> SubjectIds,
    List<string> FilePaths
) : IRequest<Result<bool>>;