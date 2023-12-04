

using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Contracts.Notifications;
using TutorCenter.Application.Contracts.Users.Learners;
using TutorCenter.Application.Contracts.Users.Tutors;

namespace TutorCenter.Web.Models;

public class DashBoardViewModel
{
    public TotalValueModel<TutorForListDto> TutorTotalValueModel{ get; set; } = new();
    public TotalValueModel<LearnerDto> StudentTotalValueModel{ get; set; } = new();

    public TotalValueModel<CourseForListDto> ClassTotalValueModel{ get; set; } = new();

    public object? ChartWeekData { get; set; }
    public object? PieWeekData1 { get; set; }
    public object? PieWeekData2 { get; set; }
    public object? DatesWeekData { get; set; }
    public AreaChartViewModel AreaChartViewModel { get; set; } = new AreaChartViewModel();
    
    public List<TutorCenter.Application.Contracts.Subjects.SubjectDto> SubjectDtos { get; set; } = new();
    public List<NotificationDto> NotificationDtos { get; set; } = new();
}

public class PieChartViewModel
{
    public object? series { get; set; } 
    public object? labels { get; set; }

    public string ByTime = Domain.ClassInformationConsts.ByTime.Today;
}
public class AreaChartViewModel
{
    public string totalRevenue = "Total Revenues";
    public string refunded = "Refunded";
    public string incoming = "Incomings";
    public string? totalRevenueSeries { get; set; } 
    public string? refundedSeries { get; set; } 
    public string? incomingSeries { get; set; } 
    public string? dates { get; set; }

    public string ByTime = Domain.ClassInformationConsts.ByTime.Today;
}