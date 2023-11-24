using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;

namespace TutorCenter.Application.Services.Authentication.Customer.Queries.Login;

public record CustomerLoginQuery
(
    string Email,
    string Password) : IRequest<Result<AuthenticationResult>>;