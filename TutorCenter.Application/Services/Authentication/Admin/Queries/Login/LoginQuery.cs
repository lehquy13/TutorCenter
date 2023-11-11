using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;

namespace TutorCenter.Application.Services.Authentication.Admin.Queries.Login;

public record LoginQuery
(
    string Email,
    string Password) : IRequest<Result<AuthenticationResult>>;

