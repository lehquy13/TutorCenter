using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Application.Services.Courses.Commands;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.NotificationConsts;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Review;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.TutorReviews.Commands;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand,Result<bool>>
{
    private readonly IRepository<TutorReview> _tutorReviewRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;
    private readonly ILogger<CreateReviewCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITutorRepository _tutorRepository;

    public CreateReviewCommandHandler(IRepository<TutorReview> tutorReviewRepository, IAppCache cache,
        ILogger<CreateReviewCommandHandler> logger, IMapper mapper, ITutorRepository tutorRepository,
        ICourseRepository courseRepository,
        IPublisher publisher, IUnitOfWork unitOfWork)
    {
        _tutorReviewRepository = tutorReviewRepository;
        this._cache = cache;
        this._logger = logger;
        this._mapper = mapper;
        _tutorRepository = tutorRepository;
        _courseRepository = courseRepository;
        this._publisher = publisher;
        this._unitOfWork = unitOfWork;
    }


    public async Task<Result<bool>> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var review = await _tutorReviewRepository.GetById(command.ReviewDto.Id);
            var tutor = await _tutorRepository.GetUserByEmail(command.TutorEmail);
            if (tutor is  null)
            {
                return Result.Fail(new NonExistUserError());
            }
            //Check if the review existed
            if (review is not null)
            {
                review.Description = command.ReviewDto.Description;
                review.Rate = command.ReviewDto.Rate;
                review.LastModificationTime = DateTime.Now;
            }
            else
            {
                command.ReviewDto.TutorId = tutor.Id;
                review = _mapper.Map<TutorReview>(command.ReviewDto);
                var cls = await _courseRepository.GetById(review.ClassInformationId);
                if (cls != null)
                {
                    //cls.TutorReviews = review;
                }
                //await _tutorReviewRepository.Insert(review);
            }

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                var message = "New tutor review for " + command.TutorEmail + " at " + review.CreationTime.ToLongDateString();
                await _publisher.Publish(new NewObjectCreatedEvent(review.Id, message, NotificationEnum.ReviewClass), cancellationToken);
            }
            
            //Update tutor's rate
            var reviews = await _tutorRepository.GetReviewsOfTutor(tutor.Id);
           
            tutor.Rate = (short)reviews.Average(x => x.Rate);
            
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return Result.Fail("Fail to update tutor's rate");
            }
            
            var defaultRequest = new GetObjectQuery<TutorForDetailDto>();
            _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when review is adding or updating." + ex.Message);
        }
    }
}