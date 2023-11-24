using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users.Tutors;

namespace TutorCenter.Application.Services.Users.Queries.Handlers.GetTutorById;

public class GetTutorByIdQuery : IRequest<Result<TutorForDetailDto>>
{
    public int ObjectId { get; set; }
}