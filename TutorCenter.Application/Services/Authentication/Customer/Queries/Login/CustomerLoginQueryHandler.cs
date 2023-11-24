using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Domain.Interfaces.Authentication;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Authentication.Customer.Queries.Login;

public class CustomerLoginQueryHandler
    : IRequestHandler<CustomerLoginQuery, Result<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMapper _mapper;

    private readonly IUserRepository _userRepository;
    private readonly IValidator _validator;

    public CustomerLoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IValidator validator,
        IUserRepository userRepository, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<AuthenticationResult>> Handle(CustomerLoginQuery query,
        CancellationToken cancellationToken)
    {
        //await Task.CompletedTask;
        //1. Check if user exist
        if (await _userRepository.GetUserByEmail(query.Email) is not { } user) return Result.Fail("User doesn't exist");
        //throw new Exception("User with an email doesn't exist");
        //2. Check if logining with right password

        if (user.Password != _validator.HashPassword(query.Password)) return Result.Fail("Wrong password");

        //3. Generate token
        var loginToken = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(_mapper.Map<UserLoginDto>(user), loginToken);
    }
}