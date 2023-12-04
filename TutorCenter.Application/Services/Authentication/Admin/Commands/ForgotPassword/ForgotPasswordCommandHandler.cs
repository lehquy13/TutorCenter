using System.Text;
using TutorCenter.Application.Services.Authentication.Admin.Commands.ForgotPassword;
using TutorCenter.Domain.Interfaces.Services;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Authentication.Commands.Register;

public class ForgotPasswordCommandHandler
    : IRequestHandler<ForgotPasswordCommand, AuthenticationResult>
{
    private readonly IEmailSender _emailSender;
    private readonly IUserRepository _userRepository;

    public ForgotPasswordCommandHandler(IEmailSender emailSender,
        IUserRepository userRepository)
    {
        _emailSender = emailSender;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(command.Email);
        if (user == null )
        {
            return new AuthenticationResult(null, "", false,
                "Email doesn't exist.");
        }

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"<p>Hello {user.FirstName} {user.LastName}</p>" +
                             $"<span> " +
                             $"Click <a href='https://localhost:7218/Authentication/ChangePassword/{user.Id}'>this link</a> to change your password" +
                             "</span>");

        await _emailSender.SendHtmlEmail(
            "hoangle.q3@gmail.com",
            "Forgot password request",
            stringBuilder.ToString()
        );
        return new AuthenticationResult(null, "", false, "");
    }
}