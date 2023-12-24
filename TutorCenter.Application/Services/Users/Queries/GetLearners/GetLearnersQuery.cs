using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users;
using TutorCenter.Application.Contracts.Users.Learners;

namespace TutorCenter.Application.Services.Users.Queries.GetLearners;

public class GetLearnersQuery : IRequest<Result<List<LearnerDto>>>
{

}
