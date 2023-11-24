using FluentResults;
using LazyCache;
using MediatR;
using TutorCenter.Domain.Repository;

namespace TutorCenter.Application.Services.Abstractions.CommandHandlers;

public abstract class DeleteCommandHandler<TCommand>
    : IRequestHandler<TCommand, Result<bool>>
    where TCommand : IRequest<Result<bool>>
{
    protected readonly IAppCache _cache;
    protected readonly IPublisher _publisher;
    protected readonly IUnitOfWork _unitOfWork;

    public DeleteCommandHandler(IUnitOfWork unitOfWork, IAppCache cache, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _publisher = publisher;
    }

    public abstract Task<Result<bool>> Handle(TCommand command, CancellationToken cancellationToken);
}