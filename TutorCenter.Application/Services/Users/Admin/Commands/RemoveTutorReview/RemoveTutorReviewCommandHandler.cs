using EduSmart.Domain.Repository;
using FluentResults;
using LazyCache;
using MediatR;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Review;

namespace CED.Application.Services.Users.Admin.Commands;

public class RemoveTutorReviewCommandHandler : IRequestHandler<RemoveTutorReviewCommand,Result<bool>>
{
    private readonly IRepository<TutorReview> _tutorReviewnRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    private readonly IAppCache _cache;

    public RemoveTutorReviewCommandHandler(IRepository<TutorReview> tutorReviewnRepository, IUnitOfWork unitOfWork,
        IPublisher publisher, IAppCache cache)
    {
        _tutorReviewnRepository = tutorReviewnRepository;
        this._unitOfWork = unitOfWork;
        this._publisher = publisher;
        this._cache = cache;
    }

    public async Task<Result<bool>> Handle(RemoveTutorReviewCommand command, CancellationToken cancellationToken)
    {
        if (command.Guid == 0)
            return false;
        if (await _tutorReviewnRepository.DeleteById(command.Guid))
        {
            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return Result.Fail("Error while deleting tutor review");
            }
            return true;
        }
        return Result.Fail("Tutor review not found");

    }
}
