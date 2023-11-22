using FluentResults;
using MapsterMapper;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.Handlers.GetAllTutors;

public class GetAllTutorsQueryHandler : GetAllQueryHandler<GetAllTutorsQuery, TutorForDetailDto>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly IUserRepository _userRepository;


    public GetAllTutorsQueryHandler(IUserRepository userRepository, ISubjectRepository subjectRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        IMapper mapper) : base(mapper)
    {
        _subjectRepository = subjectRepository;

        _userRepository = userRepository;
        _tutorMajorRepository = tutorMajorRepository;
    }

    public override async Task<Result<PaginatedList<TutorForDetailDto>>> Handle(GetAllTutorsQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        throw new InvalidOperationException("No more using!!!");
    }
}