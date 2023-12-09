using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users;

namespace TutorCenter.Application.Services.Users.Queries.GetLearners;

public class GetLearnersQuery : IRequest<Result<List<UserForListDto>>>
{

}
