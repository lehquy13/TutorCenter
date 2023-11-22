using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorCenter.Application.Common.Errors.Courses;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Services.Abstractions.CommandHandlers;
using TutorCenter.Application.Services.Courses.Commands;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.NotificationConsts;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Courses.Tutor.Commands.ApplyClass;

public class RequestGettingClassCommandHandler : CreateUpdateCommandHandler<RequestGettingClassCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseRequestRepository _requestGettingClassRepository;
    private readonly ITutorRepository _tutorRepository;

    public RequestGettingClassCommandHandler(
        ITutorRepository tutorRepository,
        ICourseRepository classInformationRepository,
        ILogger<RequestGettingClassCommandHandler> logger,
        ICourseRequestRepository requestGettingClassRepository,
        IUnitOfWork unitOfWork,
        IAppCache cache,
        IMapper mapper,
        IPublisher publisher) : base(logger, mapper, unitOfWork, cache, publisher)
    {
        _courseRepository = classInformationRepository;
        _tutorRepository = tutorRepository;
        _requestGettingClassRepository = requestGettingClassRepository;
    }

    public override async Task<Result<bool>> Handle(RequestGettingClassCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            //Check if the user existed
            var tutor = await _tutorRepository.ExistenceCheck(command.TutorId);
            if (tutor is null) return Result.Fail<bool>(new NonExistTutorError());

            var classInfor = await _courseRepository.GetById(command.ClassId);
            if (classInfor is null) return Result.Fail<bool>(new NonExistClassError());

            if (classInfor.IsDeleted is true || !classInfor.Status.Equals(Status.Available))
                return Result.Fail<bool>(new UnAvailableClassError());


            if (await _requestGettingClassRepository.IsRequested(command.ClassId, command.TutorId))
            {
                _logger.LogError("Tutor has already requested");
                return Result.Fail<bool>(new RequestedClassError());
            }

            //Create new request
            var request = new CourseRequest
            {
                CourseId = classInfor.Id,
                TutorId = tutor.Id
            };

            var entity = await _requestGettingClassRepository.Insert(request);
            if (await _unitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                var message = "New request: " + classInfor.Title + " at " + DateTime.Now.ToLongDateString();
                await _publisher.Publish(
                    new NewObjectCreatedEvent(entity.CourseId, message, NotificationEnum.RequestGettingClass),
                    cancellationToken);
                return true;
            }

            return Result.Fail("Fail to create new request");
        }
        catch (Exception e)
        {
            _logger.LogError("{0}", e.Message);
            return Result.Fail(e.Message);
        }
    }
}