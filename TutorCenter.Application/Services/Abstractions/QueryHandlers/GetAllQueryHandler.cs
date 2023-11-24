using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts;

namespace TutorCenter.Application.Services.Abstractions.QueryHandlers;

public abstract class GetAllQueryHandler<TQuery, TDto>
    : IRequestHandler<TQuery, Result<PaginatedList<TDto>>>
    where TDto : class where TQuery : IRequest<Result<PaginatedList<TDto>>>
{
    protected readonly IMapper _mapper;

    public GetAllQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public abstract Task<Result<PaginatedList<TDto>>> Handle(TQuery query, CancellationToken cancellationToken);
}

public abstract class GetAllListQueryHandler<TQuery, TDto>
    : IRequestHandler<TQuery, Result<List<TDto>>>
    where TDto : class where TQuery : IRequest<Result<List<TDto>>>
{
    protected readonly IMapper _mapper;

    public GetAllListQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public abstract Task<Result<List<TDto>>> Handle(TQuery query, CancellationToken cancellationToken);
}