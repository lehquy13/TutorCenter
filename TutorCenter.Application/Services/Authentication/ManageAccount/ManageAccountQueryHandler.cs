using CED.Domain.Interfaces.Authentication;
using CED.Domain.Users;
using MediatR;
using TutorCenter.Application.Contracts.Authentication;

namespace CED.Application.Services.Authentication.ManageAccount;

public class ManageAccountQueryHandler
    : IRequestHandler<ManageAccountQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public ManageAccountQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<AuthenticationResult> Handle(ManageAccountQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        //1. Check if user exist
        if (_jwtTokenGenerator.ValidateToken(query.Token) is false)
        {
            return new AuthenticationResult(null,"",false, "User doesn't exist");
            //throw new Exception("User with an email doesn't exist");
        }

        //2. Check if logining with right password

        //if (user.Password != query.Password)
        //{
        //    return new AuthenticationResult(null, "", false, "Wrong password");

        //}
        ////3. Generate token
        //var loginToken = _jwtTokenGenerator.GenerateToken(
        //    user.Id,
        //    user.FirstName,
        //    user.LastName);
        return new AuthenticationResult(null, "", false, "User doesn't exist");

        //return new AuthenticationResult(user, loginToken, true, "Login successfully");
    }
}

