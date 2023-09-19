using TutorCenter.Application.Contracts.Charts;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.DashBoard.Queries;

public class GetLineChartDataQuery : GetObjectQuery<LineChartData>
{
    public string ByTime { get; set; } = "";
}