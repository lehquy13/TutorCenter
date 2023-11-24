using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses.Repos;

namespace TutorCenter.Application.Services.Courses.Queries.GetCourseQuery;

public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, Result<CourseForDetailDto>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public GetCourseQueryHandler(ICourseRepository courseRepository,
        IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<Result<CourseForDetailDto>> Handle(GetCourseQuery query,
        CancellationToken cancellationToken)
    {
        var classInformation = await _courseRepository.GetById(query.Id);
        if (classInformation == null)
        {
            return Result.Fail("The class doesn't exist");
        }
        var classDto = _mapper.Map<CourseForDetailDto>(classInformation);
        return classDto;
    }
}