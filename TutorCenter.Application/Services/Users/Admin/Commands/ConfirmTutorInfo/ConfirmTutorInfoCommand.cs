using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users.Tutors;

namespace TutorCenter.Application.Services.Users.Admin.Commands.ConfirmTutorInfo;

/// <summary>
///     This command currently is not used
/// </summary>
/// <param name="TutorForDetailDto"></param>
public record ConfirmTutorInfoCommand
(
    TutorForDetailDto TutorForDetailDto
) : IRequest<Result<bool>>;