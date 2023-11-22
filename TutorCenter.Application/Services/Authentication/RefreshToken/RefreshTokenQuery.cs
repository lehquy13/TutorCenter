using FluentResults;
using MediatR;

namespace TutorCenter.Application.Services.Authentication.RefreshToken;

public record RefreshTokenQuery
(
    string Email
) : IRequest<Result<string>>;