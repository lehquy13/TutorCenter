using FluentResults;
using MapsterMapper;
using TutorCenter.Application.Contracts.Charts;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.DashBoard.Queries;

public class GetLineChartDataQueryHandler : GetByIdQueryHandler<GetLineChartDataQuery, LineChartData>
{
    private readonly ICourseRepository _classInformationRepository;
    private readonly IUserRepository _tutorRepository;
    private readonly IUserRepository _userRepository;

    public GetLineChartDataQueryHandler(IMapper mapper, ICourseRepository classInformationRepository,
        IUserRepository userRepository, IUserRepository tutorRepository) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _userRepository = userRepository;
        _tutorRepository = tutorRepository;
    }

    public override async Task<Result<LineChartData>> Handle(GetLineChartDataQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var dates = new List<int>();

        var startDay = DateTime.Today;
        switch (query.ByTime)
        {
            case "month":
                startDay = startDay.Subtract(TimeSpan.FromDays(29));

                for (var i = 0; i < 30; i++)
                {
                    dates.Add(startDay.Day);
                    startDay = startDay.AddDays(1);
                }

                startDay = startDay.Subtract(TimeSpan.FromDays(29));

                break;
            default:
                startDay = DateTime.Today.Subtract(TimeSpan.FromDays(6));

                for (var i = 0; i < 7; i++)
                {
                    dates.Add(startDay.Day);
                    startDay = startDay.AddDays(1);
                }

                startDay = DateTime.Today.Subtract(TimeSpan.FromDays(6));

                break;
        }

        var allClasses = _classInformationRepository.GetAll().Where(x => x.CreationTime >= startDay)
            .GroupBy(x => x.CreationTime.Day).ToList();
        var allLearner = _userRepository.GetAll()
            .Where(x => x.CreationTime >= startDay && x.Role == UserRole.Learner)
            .GroupBy(x => x.CreationTime.Day).ToList();
        var allTutor = _tutorRepository.GetAll()
            .Where(x => x.CreationTime >= startDay && x.Role == UserRole.Tutor).GroupBy(x => x.CreationTime.Day)
            .ToList();

        var classesInWeek = dates.Join(
                allClasses,
                d => d,
                c => c.Key,
                (d, c) => new
                {
                    dates = d,
                    classInfo = c.Count()
                })
            .Select(x => x.classInfo)
            .ToList();

        var classesInWeek1 =
        (
            from d in dates
            join c in allClasses on d equals c.Key
                into DateClassesGroup
            from cl in DateClassesGroup.DefaultIfEmpty()
            select new
            {
                classInfo = cl != null ? cl.Count() : 0
            }.classInfo
        ).ToList();

        var studentsInWeek = dates.GroupJoin(
                allLearner,
                d => d,
                c => c.Key,
                (d, c) => new
                {
                    dates = d,
                    classInfo = c.FirstOrDefault()?.Count() ?? 0
                })
            .Select(x => x.classInfo)
            .ToList();

        var tutorsInWeek = dates.GroupJoin(
                allTutor,
                d => d,
                c => c.Key,
                (d, c) => new
                {
                    dates = d,
                    classInfo = c.FirstOrDefault()?.Count() ?? 0
                })
            .Select(x => x.classInfo)
            .ToList();

        var chartWeekData = new List<LineData>
        {
            new(
                "Classes",
                classesInWeek1
            ),
            new(
                "Tutors",
                tutorsInWeek
            ),
            new(
                "Students",
                studentsInWeek
            )
        };


        return new LineChartData(chartWeekData, dates);
    }
}