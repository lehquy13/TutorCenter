using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.GetLearningCoursesOfUserQuery;

public class
    GetLearningCoursesOfUserQueryHandler : IRequestHandler<GetLearningCoursesOfUserQuery,
        Result<PaginatedList<CourseForListDto>>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetLearningCoursesOfUserQueryHandler(
        ICourseRepository classInformationRepository,
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _courseRepository = classInformationRepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<CourseForListDto>>> Handle(GetLearningCoursesOfUserQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var student = await _userRepository.GetById(query.ObjectId);
            if (student is null) throw new Exception("The user does not exist!");

            var classInformations = await _courseRepository
                .GetLearningCoursesByUserId(query.ObjectId);

            var subjects = await _subjectRepository.GetAllList();
            var tutors = _userRepository.GetTutors();

            var coursesDtos =
                _mapper.Map<List<CourseForListDto>>(classInformations.Skip((query.PageIndex - 1) * query.PageSize)
                    .Take(query.PageSize));

            var resultPaginatedList = PaginatedList<CourseForListDto>.CreateAsync(coursesDtos,
                query.PageIndex, query.PageSize, coursesDtos.Count);


            return resultPaginatedList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}