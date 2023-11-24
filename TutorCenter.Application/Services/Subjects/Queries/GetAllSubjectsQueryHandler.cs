using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Subjects;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users;

namespace TutorCenter.Application.Services.Subjects.Queries;

public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, Result<List<SubjectDto>>>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly IMapper _mapper;

    public GetAllSubjectsQueryHandler(ISubjectRepository subjectRepository,
        IRepository<TutorMajor> tutorMajorRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _tutorMajorRepository = tutorMajorRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<SubjectDto>>> Handle(GetAllSubjectsQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var subjects = await _subjectRepository.GetAllList();
            //if ObjectId is not empty, get all the subjects which is tutor's major
            if (query.ObjectId != 0)
            {
                var tutorMajors = await _subjectRepository.GetTutorMajors(query.ObjectId);
                //remove tutor's major from all subjects
                subjects = subjects.Except(tutorMajors).ToList();
            }

            var totalSubjects = subjects.Count;

            return _mapper.Map<List<SubjectDto>>(subjects);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}