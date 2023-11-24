using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Contracts.Users;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserForDetailDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserForDetailDto>> Handle(GetUserByIdQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetById(query.Id);
            if (user is null) return Result.Fail(new NonExistUserError());
            var result = _mapper.Map<UserForDetailDto>(user);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}