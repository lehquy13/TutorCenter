using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Users;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.Handlers.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<PaginatedList<UserDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<UserDto>>> Handle(GetUsersQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var users = await _userRepository.GetAllList();

            var result = _mapper.Map<List<UserDto>>(users.Where(x => x.IsDeleted is false)
                .Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize)
                .ToList());

            return PaginatedList<UserDto>.CreateAsync(result, query.PageIndex, query.PageSize, users.Count);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}