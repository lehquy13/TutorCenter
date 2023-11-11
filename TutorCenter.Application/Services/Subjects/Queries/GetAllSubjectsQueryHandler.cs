using EduSmart.Domain.Repository;
using FluentResults;
using MapsterMapper;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Subjects;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Users;

namespace TutorCenter.Application.Services.Subjects.Queries;

public class GetAllSubjectsQueryHandler : GetAllQueryHandler<GetObjectQuery<PaginatedList<SubjectDto>>, SubjectDto>// không nên như này
//public class GetAllSubjectsQueryHandler : GetAllQueryHandler<GetAllSubjectsQuery, SubjectDto>// nên như này
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;

    public GetAllSubjectsQueryHandler(ISubjectRepository subjectRepository,IRepository<TutorMajor> tutorMajorRepository, IMapper mapper):base(mapper)
    {
        _subjectRepository = subjectRepository;
        _tutorMajorRepository = tutorMajorRepository;
    }
    public override async Task<Result<PaginatedList<SubjectDto>>> Handle(GetObjectQuery<PaginatedList<SubjectDto>> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            List<Subject> subjects = await _subjectRepository.GetAllList();
            //if ObjectId is not empty, get all the subjects which is tutor's major
            if (query.ObjectId != 0)
            {
                var tutorMajors = await _subjectRepository.GetTutorMajors(query.ObjectId);
                //remove tutor's major from all subjects
                subjects = subjects.Except(tutorMajors).ToList();
            }

            var totalSubjects = subjects.Count;
           
            return PaginatedList<SubjectDto>.CreateAsync(_mapper.Map<List<SubjectDto>>(subjects),query.PageIndex,query.PageSize,totalSubjects);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

