using MediatR;

namespace TutorCenter.Application.Services.Authentication.ValidateToken;

public record ValidateTokenQuery
(
    string ValidateToken
) : IRequest<bool>;