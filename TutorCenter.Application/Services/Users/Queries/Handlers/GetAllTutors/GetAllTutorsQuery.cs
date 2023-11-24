using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Users.Tutors;

namespace TutorCenter.Application.Services.Users.Queries.Handlers.GetAllTutors;

public class GetAllTutorsQuery : IRequest<Result<PaginatedList<TutorForDetailDto>>>
{
}