using FluentResults;
using MapsterMapper;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Contracts.Users;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.CustomerQueries;

public class GetUserByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<UserForDetailDto>, UserForDetailDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository,
        IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }

    public override async Task<Result<UserForDetailDto>> Handle(GetObjectQuery<UserForDetailDto> query,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetById(query.ObjectId);
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