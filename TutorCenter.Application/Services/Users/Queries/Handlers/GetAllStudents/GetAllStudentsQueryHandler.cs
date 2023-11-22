using FluentResults;
using MapsterMapper;
using TutorCenter.Application.Contracts.Users.Learners;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.Handlers
{
    public class GetAllStudentsQueryHandler : GetAllQueryHandler<GetAllStudentsQuery, LearnerDto>
    {
        private readonly IUserRepository _userRepository;

        public GetAllStudentsQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
        {
            _userRepository = userRepository;
        }

        public override async Task<Result<PaginatedList<LearnerDto>>> Handle(GetAllStudentsQuery query,
            CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            try
            {
                var users = await _userRepository.GetLearners();
                var result = _mapper.Map<List<LearnerDto>>(users.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList());

                return PaginatedList<LearnerDto>.CreateAsync(result, query.PageIndex, query.PageSize, users.Count);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
