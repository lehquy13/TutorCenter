using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users.Learners;

namespace TutorCenter.Application.Services.Users.Student.Queries.GetLearnerById;

public record GetLearnerByIdQuery(int Id) : IRequest<Result<LearnerDto>>
{
}