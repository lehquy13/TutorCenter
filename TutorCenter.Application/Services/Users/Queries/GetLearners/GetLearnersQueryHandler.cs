using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts.Users;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.GetLearners;

public class GetLearnersQueryHandler : IRequestHandler<GetLearnersQuery, Result<List<UserForListDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetLearnersQueryHandler(IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<UserForListDto>>> Handle(GetLearnersQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetLearners();
            var result = _mapper.Map<List<UserForListDto>>(user);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}