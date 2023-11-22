using MediatR;
using TutorCenter.Application.Contracts.Subjects;

namespace TutorCenter.Application.Services.Subjects.Queries;

public class GetAllSubjectsQuery : IRequest<List<SubjectDto>>
{
}