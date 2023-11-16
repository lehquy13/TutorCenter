using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users.Tutors;

namespace TutorCenter.Application.Services.Users.Tutor.Registers;

public record TutorRegistrationCommand
(
    TutorForRegistrationDto TutorForRegistrationDto
    ) : IRequest<Result<bool>>;

