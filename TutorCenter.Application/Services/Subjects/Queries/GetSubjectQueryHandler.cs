using FluentResults;
using MapsterMapper;
using TutorCenter.Application.Contracts.Subjects;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Courses.Repos;

namespace TutorCenter.Application.Services.Subjects.Queries;

public class GetSubjectQueryHandler : GetByIdQueryHandler<GetSubjectQuery, SubjectDto>
{
    private readonly ISubjectRepository _subjectRepository;
    public GetSubjectQueryHandler(ISubjectRepository subjectRepository, IMapper mapper) : base(mapper)
    {
        _subjectRepository = subjectRepository;
    }

    public override async Task<Result<SubjectDto>> Handle(GetSubjectQuery query, CancellationToken cancellationToken)
    {
        Subject? subject = await _subjectRepository.GetById(query.Id);
        if (subject == null)
        {
            return Result.Fail("Subject does not exist");
        }
        return _mapper.Map<SubjectDto>(subject);
    }
}

