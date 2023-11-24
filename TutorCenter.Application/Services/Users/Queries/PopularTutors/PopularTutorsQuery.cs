using MediatR;
using TutorCenter.Application.Contracts.Users.Tutors;

namespace TutorCenter.Application.Services.Users.Queries.PopularTutors;

public class PopularTutorsQuery : IRequest<List<TutorForDetailDto>>
{
}