using TutorCenter.Domain.Interfaces.Authentication;
using TutorCenter.Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Authentication.Admin.Commands.ChangePassword;

public class ChangePasswordCommandCommandHandler
    : IRequestHandler<ChangePasswordCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IValidator _validator;

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    ILogger<ChangePasswordCommandCommandHandler> _logger;

    public ChangePasswordCommandCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IValidator validator,
        IUserRepository userRepository, ILogger<ChangePasswordCommandCommandHandler> logger, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<AuthenticationResult> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed


        if (await _userRepository.GetById(command.Id) is not User user)
        {
            _logger.LogError("Can not change password. User doesn't exist");
            return new AuthenticationResult(null, "", false, "User doesn't exist");
        }

        //2.1 HashPassword
        if( command.NewPassword != command.ConfirmedPassword || command.NewPassword == command.CurrentPassword ||command.CurrentPassword == command.ConfirmedPassword )
        {
            _logger.LogError("Can not change password. Confirmed Password doesn't match with NewPassword");
            return new AuthenticationResult(null, "", false, "Password doesn't match.");
        }
        if (_validator.HashPassword(command.CurrentPassword) != user.Password)
        {
            _logger.LogError("Can not change password. Password doesn't match");
            return new AuthenticationResult(null, "", false, "Password doesn't match");
        }

        user.Password = _validator.HashPassword(command.ConfirmedPassword);
        
        var newUser = _userRepository.Update(user);

        return new AuthenticationResult(_mapper.Map<UserLoginDto>(user), "", true, "Password changed.");
    }
}