using FluentResults;
using MapsterMapper;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses.Repos;

namespace TutorCenter.Application.Services.Courses.Queries.GetCourseQuery;

public class GetCourseQueryHandler : GetByIdQueryHandler<GetObjectQuery<CourseForDetailDto>, CourseForDetailDto>
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseQueryHandler(ICourseRepository courseRepository,
        IMapper mapper) : base(mapper)
    {
        _courseRepository = courseRepository;
    }

    public override async Task<Result<CourseForDetailDto>> Handle(GetObjectQuery<CourseForDetailDto> query,
        CancellationToken cancellationToken)
    {
        var classInformation = await _courseRepository.GetById(query.ObjectId);
        if (classInformation == null) return Result.Fail("The class doesn't exist");
        var classDto = _mapper.Map<CourseForDetailDto>(classInformation);
        return classDto;
    }
}