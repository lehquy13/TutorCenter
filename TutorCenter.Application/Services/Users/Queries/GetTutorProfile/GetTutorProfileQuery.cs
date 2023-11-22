using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users.Tutors;

namespace TutorCenter.Application.Services.Users.Queries.GetTutorProfile;

public class GetTutorProfileQuery : IRequest<Result<TutorProfileDto>>
{
    public int id { get; set; }
}