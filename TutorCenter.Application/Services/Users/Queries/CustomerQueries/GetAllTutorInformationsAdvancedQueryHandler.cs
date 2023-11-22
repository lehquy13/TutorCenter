using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.CustomerQueries;

public class
    GetAllTutorInformationsAdvancedQueryHandler
    : IRequestHandler<GetAllTutorInformationsAdvancedQuery, Result<PaginatedList<TutorForListDto>>>
{
    private readonly IMapper _mapper;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly ITutorRepository _tutorRepository;
    private readonly IUserRepository _userRepository;

    public GetAllTutorInformationsAdvancedQueryHandler(
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        ITutorRepository tutorRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _tutorRepository = tutorRepository;
        _tutorMajorRepository = tutorMajorRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Result<PaginatedList<TutorForListDto>>> Handle(GetAllTutorInformationsAdvancedQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var subjects = await _subjectRepository.GetAllList();
            var tutors = _tutorRepository.GetAll();

            if (query.Academic != AcademicLevel.Optional)
                tutors = tutors.Where(user => user.AcademicLevel == query.Academic);

            if (!string.IsNullOrEmpty(query.Address))
                tutors = tutors.Where(user => user.Address.Contains(query.Address));

            if (query.Gender != Gender.None) tutors = tutors.Where(user => user.Gender == query.Gender);

            if (query.BirthYear != 0) tutors = tutors.Where(user => user.BirthYear == query.BirthYear);

            if (!string.IsNullOrEmpty(query.SubjectName))
                tutors = tutors.Where(x => x.Subjects.Any(y => y.Name.Contains(query.SubjectName)));

            var tutorFromDb = tutors.ToList();
            var mergeList = _mapper.Map<List<TutorForListDto>>(tutorFromDb);

            var totalPages = tutorFromDb.Count;

            var result =
                PaginatedList<TutorForListDto>.CreateAsync(mergeList, query.PageIndex, query.PageSize, totalPages);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}