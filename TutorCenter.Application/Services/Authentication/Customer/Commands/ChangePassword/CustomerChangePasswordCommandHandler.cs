﻿using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Domain.Interfaces.Authentication;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Authentication.Customer.Commands.ChangePassword;

public class
    CustomerChangePasswordCommandHandler : IRequestHandler<CustomerChangePasswordCommand, Result<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMapper _mapper;

    private readonly IUserRepository _userRepository;
    private readonly IValidator _validator;


    private readonly ILogger<CustomerChangePasswordCommandHandler> _logger;

    public CustomerChangePasswordCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IValidator validator,
        IUserRepository userRepository, ILogger<CustomerChangePasswordCommandHandler> logger, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<AuthenticationResult>> Handle(CustomerChangePasswordCommand command,
        CancellationToken cancellationToken)
    {
        //Check if the user existed


        if (await _userRepository.GetById(command.Id) is not { } user)
        {
            _logger.LogError("Can not change password. User doesn't exist");
            return Result.Fail("User doesn't exist");
        }

        //2.1 HashPassword
        if (command.NewPassword != command.ConfirmedPassword || command.NewPassword == command.CurrentPassword)
        {
            _logger.LogError("Can not change password. Confirmed Password doesn't match with NewPassword");
            return Result.Fail("Confirmed Password doesn't match with NewPassword");
        }

        if (_validator.HashPassword(command.CurrentPassword) != user.Password)
        {
            _logger.LogError("Can not change password. Password doesn't match");
            return Result.Fail("Password doesn't match");
        }

        user.Password = _validator.HashPassword(command.ConfirmedPassword);

        var newUser = _userRepository.Update(user);

        return new AuthenticationResult(_mapper.Map<UserLoginDto>(user), _jwtTokenGenerator.GenerateToken(user));
    }
}