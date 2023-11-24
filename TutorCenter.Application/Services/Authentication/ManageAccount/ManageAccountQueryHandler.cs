using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Domain.Interfaces.Authentication;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Authentication.ManageAccount;

public class ManageAccountQueryHandler
    : IRequestHandler<ManageAccountQuery, Result<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public ManageAccountQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<Result<AuthenticationResult>> Handle(ManageAccountQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        //1. Check if user exist
        if (_jwtTokenGenerator.ValidateToken(query.Token) is false) return Result.Fail("Token is invalid");
        //throw new Exception("User with an email doesn't exist");

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
        return Result.Ok();
    }
}