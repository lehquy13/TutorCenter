using MediatR;
using TutorCenter.Application.Contracts.Authentication;

namespace CED.Application.Services.Authentication.ManageAccount;

public record ManageAccountQuery
(
   string Token
    ) : IRequest<AuthenticationResult>;

