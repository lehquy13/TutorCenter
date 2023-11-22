using TutorCenter.Application.Contracts.Charts;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.DashBoard.Queries;

/// <summary>
///     Get financial data for Area Chart
/// </summary>
/// <param name="ByTime"></param>
public class GetAreaChartDataQuery : GetObjectQuery<AreaChartData>
{
    public string ByTime = "";
}