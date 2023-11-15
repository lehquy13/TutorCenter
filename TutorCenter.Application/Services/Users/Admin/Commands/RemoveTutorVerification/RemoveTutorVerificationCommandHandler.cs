
using FluentResults;
using LazyCache;
using MediatR;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users;

namespace CED.Application.Services.Users.Admin.Commands;

public class RemoveTutorVerificationCommandHandler : IRequestHandler<RemoveTutorVerificationCommand,Result<bool>>
{
    private readonly IRepository<TutorVerificationInfo> _tutorVerificationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    private readonly IAppCache _cache;

    public RemoveTutorVerificationCommandHandler(IRepository<TutorVerificationInfo> tutorVerificationRepository,
        IUnitOfWork unitOfWork, IPublisher publisher, IAppCache cache)
    {
        _tutorVerificationRepository = tutorVerificationRepository;
        this._unitOfWork = unitOfWork;
        this._publisher = publisher;
        this._cache = cache;
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
