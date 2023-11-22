using MediatR;
using TutorCenter.Application.Contracts.Users.Tutors;

namespace TutorCenter.Application.Services.Users.Queries.CustomerQueries;

public class PopularTutorsQuery : IRequest<List<TutorForDetailDto>>
{
}