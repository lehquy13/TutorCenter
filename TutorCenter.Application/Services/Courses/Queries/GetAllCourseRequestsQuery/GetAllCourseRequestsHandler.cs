using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses.Repos;

namespace TutorCenter.Application.Services.Courses.Queries.GetAllCourseRequestsQuery;

internal class GetAllCourseRequestsHandler : IRequestHandler<GetAllCourseRequests, Result<List<CourseRequestDto>>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public GetAllCourseRequestsHandler(
        ICourseRepository courseRepository,
        IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CourseRequestDto>>> Handle(GetAllCourseRequests query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var courseInformation = await _courseRepository.GetAllClassWithRequest(query.ClassId);
            if (courseInformation is null) return Result.Fail("The class doesn't exist");

            var requestGettingClassDtos = _mapper.Map<List<CourseRequestDto>>(courseInformation.CourseRequests);
            return requestGettingClassDtos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}