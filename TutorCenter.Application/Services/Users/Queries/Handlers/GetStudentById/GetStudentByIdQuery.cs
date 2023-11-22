using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users.Learners;

namespace TutorCenter.Application.Services.Users.Queries.Handlers.GetStudentById;

public class GetStudentByIdQuery : IRequest<Result<LearnerDto>>
{
    public int ObjectId { get; set; }
}