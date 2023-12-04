using TutorCenter.Contracts.Authentication;
using TutorCenter.Domain.Interfaces.Authentication;
using TutorCenter.Domain.Users;
using MapsterMapper;
using MediatR;

namespace TutorCenter.Application.Services.Authentication.Admin.Commands.Register;

public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IValidator _validator;

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IValidator validator,
        IUserRepository userRepository, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed

        if (await _userRepository.GetUserByEmail(command.Email) is not null)
        {
            //  return new AuthenticationResult(false, "User has already existed");
            throw new Exception("User with an email has already existed");
        }

        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = _validator.HashPassword(command.Password)
        };

        var entity = await _userRepository.Insert(user);

        //Create jwt token
        var token = _jwtTokenGenerator.GenerateToken(entity);
         

        return new AuthenticationResult(_mapper.Map<UserLoginDto>(user), token, true, "Register successfully");
    }
}