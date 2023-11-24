using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Common.Errors.Courses;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses.Repos;

namespace TutorCenter.Application.Services.Courses.Tutor.Queries.GetTeachingClassDetailQuery;

public class GetTeachingClassDetailQueryHandler
    : IRequestHandler<GetTeachingClassDetailQuery, Result<RequestGettingClassExtendDto>>

{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public GetTeachingClassDetailQueryHandler(
        ICourseRepository classInformationRepository,
        IMapper mapper)
    {
        _courseRepository = classInformationRepository;
        _mapper = mapper;
    }

    public async Task<Result<RequestGettingClassExtendDto>> Handle(GetTeachingClassDetailQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var course = await _courseRepository.GetById(query.CourseId);
            if (course is null) return Result.Fail(new NonExistClassError());

            var requestFromDb = course.CourseRequests.FirstOrDefault(x => x.Id == query.CourseId);
            if (requestFromDb is null) return Result.Fail(new NonExistRequestError());
            var resylt = _mapper.Map<RequestGettingClassExtendDto>(requestFromDb);
            return resylt;
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}