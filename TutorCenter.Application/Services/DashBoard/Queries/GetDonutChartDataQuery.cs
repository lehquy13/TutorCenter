using TutorCenter.Application.Contracts.Charts;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.DashBoard.Queries;

public class GetDonutChartDataQuery : GetObjectQuery<DonutChartData>
{
    public string ByTime { get; set; } = "";
}