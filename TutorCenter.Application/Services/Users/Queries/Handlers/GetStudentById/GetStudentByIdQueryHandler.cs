using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Contracts.Users.Learners;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.Handlers.GetStudentById;

//Not using currently
public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Result<LearnerDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetStudentByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<LearnerDto>> Handle(GetStudentByIdQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetById(query.ObjectId);
            if (user is null) return Result.Fail(new NonExistUserError());
            var result = _mapper.Map<LearnerDto>(user);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}