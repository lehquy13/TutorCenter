using FluentResults;
using LazyCache;
using MediatR;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Review;

namespace TutorCenter.Application.Services.Users.Admin.Commands.RemoveTutorReview;

public class RemoveTutorReviewCommandHandler : IRequestHandler<RemoveTutorReviewCommand, Result<bool>>
{
    private readonly IAppCache _cache;
    private readonly IPublisher _publisher;
    private readonly IRepository<TutorReview> _tutorReviewnRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveTutorReviewCommandHandler(IRepository<TutorReview> tutorReviewnRepository, IUnitOfWork unitOfWork,
        IPublisher publisher, IAppCache cache)
    {
        _tutorReviewnRepository = tutorReviewnRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _cache = cache;
    }

    public async Task<Result<bool>> Handle(RemoveTutorReviewCommand command, CancellationToken cancellationToken)
    {
        if (command.Guid == 0)
            return false;
        if (await _tutorReviewnRepository.DeleteById(command.Guid))
        {
            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
                return Result.Fail("Error while deleting tutor review");
            return true;
        }

        return Result.Fail("Tutor review not found");
    }
}