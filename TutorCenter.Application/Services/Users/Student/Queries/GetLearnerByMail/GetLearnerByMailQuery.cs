using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users.Learners;

namespace TutorCenter.Application.Services.Users.Student.Queries.GetLearnerByMail;

public class GetLearnerByMailQuery : IRequest<Result<LearnerDto>>
{
    public string Email = string.Empty;
}