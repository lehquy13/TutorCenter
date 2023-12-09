using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Courses.Queries.GetAllCoursesQuery;

public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, Result<PaginatedList<CourseForListDto>>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllCoursesQueryHandler(
        ICourseRepository courseRepository,
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _courseRepository = courseRepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<CourseForListDto>>> Handle(GetAllCoursesQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            //Create a list of class query
            var classesQuery = _courseRepository.GetAll()
                .OrderByDescending(x => x.CreationTime)
                .Where(x => x.IsDeleted == false);
            //Filter by Today | Verifying | Purchasing | All
            switch (query.Filter)
            {
                case "Today":
                    classesQuery = classesQuery.Where(x => x.CreationTime >= DateTime.Today);
                    break;

                case "Verifying":
                    classesQuery = classesQuery.Where(x => x.Status == Status.OnVerifying);
                    break;
                case "Purchasing":
                    classesQuery = classesQuery.Where(x => x.Status == Status.OnPurchasing);
                    break;
            }


            //Filter by SubjectName if it is not null
            if (!string.IsNullOrWhiteSpace(query.SubjectName))
            {
                var subject = await _subjectRepository.GetSubjectByName(query.SubjectName);

                if (subject is not null) classesQuery = classesQuery.Where(x => x.SubjectId == subject.Id);
            }

            //Filter by Status if it is not null
            if (query.Status is not null) classesQuery = classesQuery.Where(x => x.Status == query.Status);

            var classesQueryResult = classesQuery.Include(x => x.Subject)
                .ToList();

            //totalPages after filtering
            var totalPages = classesQuery.Count();

            //Get the class of the page
            var courseDtos =
                _mapper.Map<List<CourseForListDto>>(classesQuery.Skip((query.PageIndex - 1) * query.PageSize)
                    .Take(query.PageSize).ToList());

            //transform the class of the page to PaginatedList
            var resultPaginatedList = PaginatedList<CourseForListDto>.CreateAsync(courseDtos,
                query.PageIndex, query.PageSize, totalPages);

            //Map subject name attrs and tutor information attrs to the class of the page
            // foreach (var classIn in resultPaginatedList)
            // {
            //     if (await _subjectRepository.GetById(classIn.SubjectId) is { } subject)
            //     {
            //         classIn.SubjectName = subject.Name;
            //     }
            // }

            return resultPaginatedList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}