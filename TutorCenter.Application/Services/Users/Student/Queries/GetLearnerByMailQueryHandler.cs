using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Contracts.Users.Learners;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Student.Queries;

public class GetLearnerByMailQueryHandler : IRequestHandler<GetLearnerByMailQuery, Result<LearnerDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICourseRepository _classInformationRepository;
    private readonly IMapper _mapper;

    public GetLearnerByMailQueryHandler(ICourseRepository classInformationRepository,
        IMapper mapper, IUserRepository userRepository)
    {
        _classInformationRepository = classInformationRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Result<LearnerDto>> Handle(GetLearnerByMailQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(query.Email);

            if (user is null)
            {
                return Result.Fail(new NonExistUserError());
            }

            var classInfors = await _classInformationRepository.GetLearningCoursesByUserId(user.Id);

            var learner = _mapper.Map<LearnerDto>(user);
            learner.LearningCourses = PaginatedList<CourseForListDto>.CreateAsync(
                _mapper.Map<List<CourseForListDto>>(classInfors),
                1,
                100
            );

            return learner;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}