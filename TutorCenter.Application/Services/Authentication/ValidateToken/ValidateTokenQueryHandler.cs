using MediatR;
using TutorCenter.Domain.Interfaces.Authentication;

namespace TutorCenter.Application.Services.Authentication.ValidateToken;

public class ValidateTokenQueryHandler
    : IRequestHandler<ValidateTokenQuery, bool>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public ValidateTokenQueryHandler(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<bool> Handle(ValidateTokenQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _jwtTokenGenerator.ValidateToken(query.ValidateToken);
    }
}