using CED.Domain.Interfaces.Authentication;
using CED.Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorCenter.Application.Contracts.Authentication;
using TutorCenter.Application.Services.Authentication.Customer.Commands.ChangePassword;

namespace CED.Application.Services.Authentication.Customer.Commands.ChangePassword;

public class CustomerChangePasswordCommandHandler : IRequestHandler<CustomerChangePasswordCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IValidator _validator;

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    

    ILogger<CustomerChangePasswordCommandHandler> _logger;
    public CustomerChangePasswordCommandHandler(IJwtTokenGenerator jwtTokenGenerator,IValidator validator,
        IUserRepository userRepository, ILogger<CustomerChangePasswordCommandHandler> logger, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<AuthenticationResult> Handle(CustomerChangePasswordCommand command, CancellationToken cancellationToken)
    {

        //Check if the user existed


        if (await _userRepository.GetById(command.Id) is not {} user  )
        {
            _logger.LogError("Can not change password. User doesn't exist");
            return new AuthenticationResult(null,"",false, "User doesn't exist");
        }
        
        //2.1 HashPassword
        if( command.NewPassword != command.ConfirmedPassword || command.NewPassword == command.CurrentPassword)
        {
            _logger.LogError("Can not change password. Confirmed Password doesn't match with NewPassword");
            return new AuthenticationResult(null, "", false, "Password doesn't match.");
        }

        if (_validator.HashPassword(command.CurrentPassword) != user.Password)
        {
            _logger.LogError("Can not change password. Password doesn't match");
            return new AuthenticationResult(null, "", false, "Password doesn't match.");
        }
        
        user.Password = _validator.HashPassword(command.ConfirmedPassword);
        
        var newUser = _userRepository.Update(user);
        
        return new AuthenticationResult(_mapper.Map<UserLoginDto>(user), "" ,true, "Password changed.");
    }
}

