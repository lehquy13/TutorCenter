using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Domain.Interfaces.Authentication;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Authentication.Admin.Queries.Login;

public class LoginQueryHandler
    : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IValidator _validator;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IValidator validator, IUserRepository userRepository, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<Result<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        //await Task.CompletedTask;
        //1. Check if user exist
        if (await _userRepository.GetUserByEmail(query.Email) is not User user)
        {
           return Result.Fail("User with an email doesn't exist");
            //throw new Exception("User with an email doesn't exist");
        }

        //2. Check if logining with right password
        //2.1 HashPassword
        var hashPassword = _validator.HashPassword(query.Password);
        //2.2 check HashPassword
        if (user.Password != hashPassword)// || user.Role != UserRole.Admin)
        {
            return Result.Fail("Password doesn't match");
        }
        //3. Generate token
        var loginToken = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult( _mapper.Map<UserLoginDto>(user), loginToken);
    }
}

