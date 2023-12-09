using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Courses.Queries.GetCoureRequestById;

// Note: check this query handler later bc of it may violate DDD
public class GetRequestGettingClassDetailQueryHandler
    : IRequestHandler<GetCourseRequestByIdQuery, Result<CourseRequestDto>>

{
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseRequestRepository _courseRequestRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetRequestGettingClassDetailQueryHandler(
        ICourseRepository classInformationRepository,
        ICourseRequestRepository courseRequestRepository,
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _courseRepository = classInformationRepository;
        _courseRequestRepository = courseRequestRepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<CourseRequestDto>> Handle(GetCourseRequestByIdQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var requests = await _courseRequestRepository.GetById(query.Id);
            if (requests is null)
            {
                return Result.Fail("This getting class request does not exist!");
            }

            var result = _mapper.Map<CourseRequestDto>(requests);

            return result;
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}