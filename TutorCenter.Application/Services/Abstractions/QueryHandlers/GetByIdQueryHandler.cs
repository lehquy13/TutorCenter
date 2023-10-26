using FluentResults;
using MapsterMapper;
using MediatR;

namespace TutorCenter.Application.Services.Abstractions.QueryHandlers;

public abstract class GetCourseQueryHandler<TQuery, TDto>
    : IRequestHandler<TQuery, Result<TDto>>
    where TQuery : IRequest<Result<TDto>>
{
    protected readonly IMapper _mapper;

    public GetCourseQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public abstract Task<Result<TDto>> Handle(TQuery request, CancellationToken cancellationToken);

}


