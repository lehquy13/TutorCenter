using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.Handlers.GetAllTutors;

public class GetAllTutorsQueryHandler : IRequestHandler<GetAllTutorsQuery, Result<PaginatedList<TutorForDetailDto>>>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;


    public GetAllTutorsQueryHandler(IUserRepository userRepository, ISubjectRepository subjectRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        IMapper mapper)
    {
        _subjectRepository = subjectRepository;

        _userRepository = userRepository;
        _tutorMajorRepository = tutorMajorRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<TutorForDetailDto>>> Handle(GetAllTutorsQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        throw new InvalidOperationException("No more using!!!");
    }
}