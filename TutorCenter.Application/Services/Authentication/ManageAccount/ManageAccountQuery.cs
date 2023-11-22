using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;

namespace TutorCenter.Application.Services.Authentication.ManageAccount;

public record ManageAccountQuery
(
    string Token
) : IRequest<Result<AuthenticationResult>>;