using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSmart.Domain.Repository;
using FluentResults;
using LazyCache;
using MediatR;
using Newtonsoft.Json;
using TutorCenter.Application.Services.Abstractions.CommandHandlers;
using TutorCenter.Application.Services.Courses.Queries;
using TutorCenter.Application.Services.Courses.Queries.GetAllCoursesQuery;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Repository;

namespace TutorCenter.Application.Services.Courses.Commands
{
    public class DeleteCourseCommandHandler : DeleteCommandHandler<DeleteCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        public DeleteCourseCommandHandler(ICourseRepository courseRepository, IAppCache cache, IPublisher publisher, IUnitOfWork unitOfWork)
            : base(unitOfWork, cache, publisher)
        {
            _courseRepository = courseRepository;
        }
        public override async Task<Result<bool>> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
        {
            //Check if the class existed
            Course? course = await _courseRepository.GetById(command.GuidId);
            if (course is null)
            {
                return Result.Fail("Class doesn't exist");
            }
            course.IsDeleted = true;
            if (await _unitOfWork.SaveChangesAsync() > 0)
            {
                //Remove the cache
                var defaultRequest = new GetAllCoursesQuery();
                _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));
                return Result.Ok(true)
                    .WithSuccess(new Success("Delete class successfully"));
            }

            return Result.Fail("Fail to delete class");
        }
    }
}
