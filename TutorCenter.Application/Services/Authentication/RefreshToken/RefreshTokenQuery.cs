using FluentResults;
using MediatR;

namespace CED.Application.Services.Authentication.RefreshToken;

public record RefreshTokenQuery
(
    string Email
    ) : IRequest<Result<string>>;

