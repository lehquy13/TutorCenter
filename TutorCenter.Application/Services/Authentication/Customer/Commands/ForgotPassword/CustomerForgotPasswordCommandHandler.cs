using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Domain.Interfaces.Authentication;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Authentication.Customer.Commands.ForgotPassword;

public class CustomerForgotPasswordCommandHandler
    : IRequestHandler<CustomerForgotPasswordCommand, Result<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public CustomerForgotPasswordCommandHandler(IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<Result<AuthenticationResult>> Handle(CustomerForgotPasswordCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Result.Fail("Not implemented");
    }
}