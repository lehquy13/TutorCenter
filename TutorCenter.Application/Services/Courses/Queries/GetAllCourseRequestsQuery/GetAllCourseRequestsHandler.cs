using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses;
using FluentResults;
using MapsterMapper;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Domain.Courses.Repos;

namespace TutorCenter.Application.Services.Courses.Queries.GetAllCourseRequestsQuery
{
    internal class GetAllCourseRequestsHandler : GetAllListQueryHandler<GetAllCourseRequests, CourseRequestDto>
    {
        private readonly ICourseRepository _courseRepository;
        public GetAllCourseRequestsHandler(
            ICourseRepository courseRepository,
            IMapper mapper) : base(mapper)
        {
            _courseRepository = courseRepository;
        }

        public override async Task<Result<List<CourseRequestDto>>> Handle(GetAllCourseRequests query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            try
            {

                var courseInformation = await _courseRepository.GetAllClassWithRequest(query.ClassId);
                if (courseInformation is null)
                {
                    return Result.Fail("The class doesn't exist");
                }

                var requestGettingClassDtos = _mapper.Map<List<CourseRequestDto>>(courseInformation.CourseRequests);
                return requestGettingClassDtos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
