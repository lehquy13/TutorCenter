using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TutorCenter.Application.Services.Abstractions.CommandHandlers;
using TutorCenter.Application.Services.Courses.Queries.GetAllCoursesQuery;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.NotificationConsts;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Courses.Commands;

public class CreateUpdateCourseCommandHandler
    : CreateUpdateCommandHandler<CreateUpdateCourseCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;

    public CreateUpdateCourseCommandHandler(
        ICourseRepository courseRepository,
        IUnitOfWork unitOfWork,
        IAppCache cache,
        IPublisher publisher,
        ILogger<CreateUpdateCourseCommandHandler> logger, IMapper mapper, IUserRepository userRepository)
        : base(logger, mapper, unitOfWork, cache, publisher)
    {
        _courseRepository = courseRepository;
        _userRepository = userRepository;
    }

    public override async Task<Result<bool>> Handle(CreateUpdateCourseCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var course = await _courseRepository.GetById(command.CourseDto.Id);

            //Check if the class existed
            if (course is not null)
            {
                course = _mapper.Map<Course>(command.CourseDto);

                //update last modification time
                course.LastModificationTime = DateTime.Now;
                if (course.TutorId != null) course.Status = Status.Confirmed;

                //Update existed class
                var requestGettingCoursesFromDb =
                    (await _courseRepository
                        .GetCourseRequestsByClassId(course
                            .Id)) // get all request getting classes by class ObjectId
                    .Where(x => x.TutorId != course.TutorId); // get all other request in order to cancel them
                // Cancel them 
                foreach (var iClass in requestGettingCoursesFromDb) iClass.RequestStatus = RequestStatus.Canceled;

                if (await _unitOfWork.SaveChangesAsync() > 0) return true;

                return Result.Fail<bool>("Update class and it's requests failed.");
            }

            //Create new Class
            course = _mapper.Map<Course>(command.CourseDto);

            if (!string.IsNullOrWhiteSpace(command.Email))
            {
                //Class was created by a system user
                var user = await _userRepository.GetUserByEmail(command.Email);
                if (user != null) course.LearnerId = user.Id;
            }

            //update last modification time
            course.LastModificationTime = DateTime.Now;
            //update creation time bc it is new record
            course.CreationTime = DateTime.Now;

            //Handle publish event to notification service
            var entity = await _courseRepository.Insert(course);
            if (!(await _unitOfWork.SaveChangesAsync() > 0))
                return Result.Fail("Fail to save changes while creating new class.");
            var message = "New class: " + entity.Title + " at " + entity.CreationTime.ToLongDateString();
            await _publisher.Publish(
                new NewObjectCreatedEvent(entity.Id, message, NotificationEnum.Course),
                cancellationToken);

            // Clear cache
            var defaultRequest = new GetAllCoursesQuery();
            _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));
            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            return Result.Fail("Error happens when class is adding or updating." + ex.Message);
        }
    }
}