using FluentResults;
using MediatR;

namespace TutorCenter.Application.Services.Abstractions.QueryHandlers;

public class GetObjectQuery<TDto> : IRequest<Result<TDto>> where TDto : class
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 100;
    /// <summary>
    /// if(ObjectId == int.Empty) => GetAll
    /// </summary>
    public int ObjectId { get; set; }= 0;
}



