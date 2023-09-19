using CED.Domain.Interfaces.Authentication;
using CED.Domain.Users;
using MediatR;
using TutorCenter.Application.Contracts.Authentication;

namespace CED.Application.Services.Authentication.Customer.Commands.ForgotPassword;

public class CustomerForgotPasswordCommandHandler
    : IRequestHandler<CustomerForgotPasswordCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public CustomerForgotPasswordCommandHandler(IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<AuthenticationResult> Handle(CustomerForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return new AuthenticationResult(null, "",false,"");
    }
}

