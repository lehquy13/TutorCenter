using MediatR;

namespace CED.Application.Services.Authentication.ValidateToken;

public record ValidateTokenQuery
(
    string ValidateToken
    ) : IRequest<bool>;

