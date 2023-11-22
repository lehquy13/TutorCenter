using FluentResults;
using LazyCache;
using MediatR;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users;

namespace TutorCenter.Application.Services.Users.Admin.Commands.RemoveTutorVerification;

public class RemoveTutorVerificationCommandHandler : IRequestHandler<RemoveTutorVerificationCommand, Result<bool>>
{
    private readonly IAppCache _cache;
    private readonly IPublisher _publisher;
    private readonly IRepository<TutorVerificationInfo> _tutorVerificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveTutorVerificationCommandHandler(IRepository<TutorVerificationInfo> tutorVerificationRepository,
        IUnitOfWork unitOfWork, IPublisher publisher, IAppCache cache)
    {
        _tutorVerificationRepository = tutorVerificationRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _cache = cache;
    }

    public async Task<Result<bool>> Handle(RemoveTutorVerificationCommand command, CancellationToken cancellationToken)
    {
        if (command.Guid == 0)
            return false;
        if (await _tutorVerificationRepository.DeleteById(command.Guid))
        {
            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
                return Result.Fail("Error while deleting tutor verification");
            return true;
        }

        return Result.Fail("Tutor verification not found");
    }
}