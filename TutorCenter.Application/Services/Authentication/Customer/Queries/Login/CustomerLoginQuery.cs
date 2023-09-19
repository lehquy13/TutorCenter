using MediatR;
using TutorCenter.Application.Contracts.Authentication;

namespace CED.Application.Services.Authentication.Customer.Queries.Login;

public record CustomerLoginQuery
(
    string Email,
    string Password) : IRequest<AuthenticationResult>;

