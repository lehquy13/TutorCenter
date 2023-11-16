using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorCenter.Domain.Repository;

namespace TutorCenter.Application.Services.Abstractions.CommandHandlers;


public abstract class CreateUpdateCommandHandler<TCommand>
    : IRequestHandler<TCommand, Result<bool>>
    where TCommand : IRequest<Result<bool>>
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IAppCache _cache;
    protected readonly IPublisher _publisher;
    protected readonly ILogger<CreateUpdateCommandHandler<TCommand>> _logger;

    public CreateUpdateCommandHandler(ILogger<CreateUpdateCommandHandler<TCommand>> logger,IMapper mapper, IUnitOfWork unitOfWork, IAppCache cache, IPublisher publisher)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _cache = cache;
        _publisher = publisher;
    }

    public abstract Task<Result<bool>> Handle(TCommand request, CancellationToken cancellationToken);

}

