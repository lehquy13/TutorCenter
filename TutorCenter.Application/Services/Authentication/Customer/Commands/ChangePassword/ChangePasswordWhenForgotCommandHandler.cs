using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorCenter.Domain.Interfaces.Authentication;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Authentication.Customer.Commands.ChangePassword;

public class ChangePasswordWhenForgotCommandHandler : IRequestHandler<ChangePasswordWhenForgotCommand, Result<bool>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMapper _mapper;

    private readonly IUserRepository _userRepository;
    private readonly IValidator _validator;


    private readonly ILogger<ChangePasswordWhenForgotCommandHandler> _logger;

    public ChangePasswordWhenForgotCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IValidator validator,
        IUserRepository userRepository, ILogger<ChangePasswordWhenForgotCommandHandler> logger, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(ChangePasswordWhenForgotCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        if (await _userRepository.GetById(command.Id) is not User user)
        {
            _logger.LogError("Can not change password. User doesn't exist");
            return false;
        }

        user.Password = _validator.HashPassword(command.NewPassword);

        var newUser = _userRepository.Update(user);

        return true;
    }
}