using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Domain.Interfaces.Authentication;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Authentication.RefreshToken;

public class RefreshTokenQueryHandler: IRequestHandler<RefreshTokenQuery, Result<string> >
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public RefreshTokenQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(RefreshTokenQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (await _userRepository.GetUserByEmail(query.Email) is { } user)
        {
            var token = _jwtTokenGenerator.GenerateToken(user);
            return Result.Ok(token);
        }

        return Result.Fail(new NonExistUserError());
    }
}

