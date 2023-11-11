using EduSmart.Domain.Repository;
using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Interfaces.Authentication;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Authentication.Customer.Commands.Register;

public class CustomerRegisterCommandHandler 
    : IRequestHandler<CustomerRegisterCommand, Result<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IValidator _validator;

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CustomerRegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IValidator validator,
        IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<AuthenticationResult>> Handle(CustomerRegisterCommand command, CancellationToken cancellationToken)
    {

        //Check if the user existed

        if (await _userRepository.GetUserByEmail(command.Email) is not null)
        {
            //  return new AuthenticationResult(false, "User has already existed");
            return Result.Fail("User has already existed");
            throw new Exception("User with an email has already existed");
        }
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password =   _validator.HashPassword(command.Password),
            Role = UserRole.Learner,
            Address = command.Address,
            PhoneNumber = command.PhoneNumber,
            BirthYear = command.BirthYear,
            Gender = command.Gender
        };

        await _userRepository.Insert(user);
        if(await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
        {
            return Result.Fail("Can not register");
        }
        //Create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(_mapper.Map<UserLoginDto>(user), token);
    }
}

