using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorCenter.Application.Services.Abstractions.CommandHandlers;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Repository;

namespace TutorCenter.Application.Services.Courses.Commands;
public class CancelRequestGettingCourseCommandHandler : CreateUpdateCommandHandler<CancelRequestGettingCourseCommand>
{
    private readonly IRepository<CourseRequest> _requestGettingCourseRepository;

    public CancelRequestGettingCourseCommandHandler(
        ILogger<CancelRequestGettingCourseCommandHandler> logger,
        IUnitOfWork unitOfWork,
        IPublisher publisher,
        IAppCache cache,
        IMapper mapper,
        IRepository<CourseRequest> requestGettingCourseRepository
    ) : base(logger, mapper, unitOfWork, cache, publisher)
    {
        _requestGettingCourseRepository = requestGettingCourseRepository;
    }

    public override async Task<Result<bool>> Handle(CancelRequestGettingCourseCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var requestGettingCoursesFromDb = await _requestGettingCourseRepository.GetById(command.RequestGettingClassMinimalDto.Id);
        if (requestGettingCoursesFromDb is not null)
        {
            requestGettingCoursesFromDb = _mapper.Map<CourseRequest>(command.RequestGettingClassMinimalDto);
        }

        if (await _unitOfWork.SaveChangesAsync(cancellationToken) > 0)
        {
            return true;
        }

        return false;
    }
}