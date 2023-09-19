using FluentResults;
using MapsterMapper;
using TutorCenter.Application.Contracts.Charts;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses.Repos;

namespace TutorCenter.Application.Services.DashBoard.Queries;

public class GetDonutChartDataQueryHandler : GetByIdQueryHandler<GetDonutChartDataQuery, DonutChartData>
{
    private readonly ICourseRepository _classInformationRepository;
    public GetDonutChartDataQueryHandler(IMapper mapper, ICourseRepository classInformationRepository) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<Result<DonutChartData>> Handle(GetDonutChartDataQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var startDay = DateTime.Today;
        switch (query.ByTime)
        {
            case ByTime.Month:
                startDay = DateTime.Today.Subtract(TimeSpan.FromDays(29));

                break;
            case ByTime.Week:
                startDay = DateTime.Today.Subtract(TimeSpan.FromDays(6));
                break;
        }

        var classInforsPie = _classInformationRepository.GetAll()
            .Where(x => x.IsDeleted == false && x.LastModificationTime >= startDay)
            .GroupBy(x => x.Status)
            .Select((x) => new { key = x.Key.ToString(), count = x.Count()})
            .ToList();


        List<int> resultInts = classInforsPie
            .Select(x => x.count)
            .ToList();
        if (resultInts.Count <=0)
        {
            resultInts.Add(1);
        }
        List<string> resultStrings = classInforsPie
            .Select(x => x.key)
            .ToList();
        if (resultStrings.Count <= 0)
        {
            resultStrings.Add("None");
        }

        return new DonutChartData(resultInts, resultStrings);
    }
}