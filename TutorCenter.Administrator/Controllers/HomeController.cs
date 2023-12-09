using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using TutorCenter.Administrator.Models;
using MapsterMapper;
using Newtonsoft.Json;
using TutorCenter.Administrator.Utilities;
using TutorCenter.Application.Contracts.Charts;
using TutorCenter.Application.Contracts.Users.Learners;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Application.Services.DashBoard.Queries;
using TutorCenter.Application.Services.Users.Queries.GetAllTutorInformationsAdvanced;
using TutorCenter.Domain.ClassInformationConsts;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using TutorCenter.Application.Services.Courses.Queries.GetAllCoursesQuery;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Users.Queries.Handlers.GetAllStudents;

namespace TutorCenter.Administrator.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, ISender sender)
        {
            _logger = logger;
            _mapper = mapper;
            _sender = sender;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var user = User.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;
            _logger.LogDebug("Index's running! On getting classDtos, tutorDtos, studentDtos...");
            var classDtos = await _sender.Send(new GetAllCoursesQuery());
            var tutorDtos = await _sender.Send(new GetAllTutorInformationsAdvancedQuery());
            var studentDtos = await _sender.Send(new GetAllStudentsQuery());
            _logger.LogDebug("Got classDtos, tutorDtos, studentDtos!");

            var date = GetByTime(DateTime.Now, ByTime.Month);
            var thisMonthClassInformationDtoCount = classDtos.Value.Count(x => x.CreationTime >= date);
            var thisMonthTutorDtoCount = tutorDtos.Value.Count(x => x.CreationTime >= date);
            var thisMonthStudentDtoCount = studentDtos.Value.Count(x => x.CreationTime >= date);

            date = GetByTime(date, ByTime.Month);
            var lastMonthClassInformationDtoCount = classDtos.Value.Count(x => x.CreationTime >= date);
            var lastMonthResultTutorDtoCount = tutorDtos.Value.Count(x => x.CreationTime >= date);
            var lastMonthStudentDtoCount = studentDtos.Value.Count(x => x.CreationTime >= date);


            _logger.LogDebug("On getting lineChartData, donutChartData...");
            var lineChartData = await _sender.Send(new GetLineChartDataQuery());
            var donutChartData = await _sender.Send(new GetDonutChartDataQuery());
            var datesWeekData = new ChartDataType(
                "string",
                lineChartData.Value.dates
            );
            _logger.LogDebug("Got lineChartData, donutChartData!");

            var chartWeekData = JsonConvert.SerializeObject(lineChartData.Value.LineDatas);
            var pieWeekDataNames = JsonConvert.SerializeObject(donutChartData.Value.names);
            var pieWeekDataValues = JsonConvert.SerializeObject(donutChartData.Value.values);
            var dateWeekData = JsonConvert.SerializeObject(datesWeekData);

            var areaListData = await AreaChartDataCalculate(ByTime.Week);

            _logger.LogDebug("On getting recent activities...");
            var notificationDtos = await _sender.Send(new GetNotificationQuery());

            _logger.LogInformation("Got recent activities! Serializing and return");
            return View(
                new DashBoardViewModel
                {
                    StudentTotalValueModel = new TotalValueModel<LearnerDto>()
                    {
                        Models = studentDtos.Value,
                        IsIncrease = thisMonthStudentDtoCount > lastMonthStudentDtoCount,
                        IncreasePercentage = Math.Abs(thisMonthStudentDtoCount - lastMonthStudentDtoCount) * 1.0 /
                                             lastMonthStudentDtoCount * 100,
                        Time = ByTime.Month
                    },
                    ClassTotalValueModel = new TotalValueModel<CourseForListDto>()
                    {
                        Models = classDtos.Value,
                        IsIncrease = thisMonthClassInformationDtoCount > lastMonthClassInformationDtoCount,
                        IncreasePercentage =
                            Math.Abs(thisMonthClassInformationDtoCount - lastMonthClassInformationDtoCount) * 1.0 /
                            lastMonthClassInformationDtoCount * 100,
                        Time = ByTime.Month
                    },
                    TutorTotalValueModel = new TotalValueModel<TutorForListDto>()
                    {
                        Models = tutorDtos.Value,
                        IsIncrease = thisMonthTutorDtoCount > lastMonthResultTutorDtoCount,
                        IncreasePercentage = Math.Abs(thisMonthTutorDtoCount - lastMonthResultTutorDtoCount) * 1.0 /
                                             lastMonthResultTutorDtoCount * 100,
                        Time = ByTime.Month
                    },

                    ChartWeekData = chartWeekData,
                    // Chart
                    PieWeekData1 = pieWeekDataValues,
                    PieWeekData2 = pieWeekDataNames,
                    DatesWeekData = dateWeekData,
                    //Incomes Chart
                    AreaChartViewModel = new AreaChartViewModel()
                    {
                        dates = areaListData.ElementAt(0),
                        totalRevenueSeries = areaListData.ElementAt(1),
                        refundedSeries = areaListData.ElementAt(2),
                        incomingSeries = areaListData.ElementAt(3),
                        ByTime = ByTime.Week
                    },
                    NotificationDtos = notificationDtos.Value
                }
            );
        }

        [HttpGet]
        [Route("FitlerLineChart/{byTime?}")]
        public async Task<IActionResult> FitlerLineChart(string? byTime)
        {
            _logger.LogDebug("On getting lineChartData...");
            var lineChartData = await _sender.Send(new GetLineChartDataQuery
            {
                ByTime = byTime ?? ""
            });
            var datesWeekData = new ChartDataType(
                "string",
                lineChartData.Value.dates
            );
            var check = JsonConvert.SerializeObject(lineChartData.Value.LineDatas);
            var check1 = JsonConvert.SerializeObject(datesWeekData);

            return Json(new
            {
                ChartWeekData = check,
                DatesWeekData = check1
            });
        }

        [HttpGet]
        [Route("FitlerPieChart/{byTime?}")]
        public async Task<IActionResult> FitlerPieChart(string? byTime)
        {
            _logger.LogDebug("FitlerPieChart's running! On getting DonutChartData...");
            var donutChartData = await _sender.Send(new GetDonutChartDataQuery
            {
                ByTime = byTime ?? ""
            });
            _logger.LogDebug("Got donutChartData! Serializing and return.");

            var check2 = JsonConvert.SerializeObject(donutChartData.Value.names);
            var check1 = JsonConvert.SerializeObject(donutChartData.Value.values);

            return Helper.RenderRazorViewToString(this, "_PieChart",
                new PieChartViewModel()
                {
                    labels = check2,
                    series = check1,
                    ByTime = byTime ?? ByTime.Today
                });
        }

        private async Task<List<string>> AreaChartDataCalculate(string? byTime)
        {
            _logger.LogDebug("FitlerPieChart's running! On getting DonutChartData...");
            var areaChartData = await _sender.Send(new GetAreaChartDataQuery
            {
                ByTime = byTime ?? ""
            });
            _logger.LogDebug("Got donutChartData! Serializing and return");

            var check1 = JsonConvert.SerializeObject(areaChartData.Value.Dates);
            var check2 = JsonConvert.SerializeObject(areaChartData.Value.TotalRevuenues.data);
            var check3 = JsonConvert.SerializeObject(areaChartData.Value.Cenceleds.data);
            var check4 = JsonConvert.SerializeObject(areaChartData.Value.Incoming.data);

            return new List<string>
        {
            check1, check2, check3, check4
        };

        }
        [HttpGet]
        [Route("FilterAreaChart/{byTime?}")]
        public async Task<IActionResult> FilterAreaChart(string? byTime)
        {
            var listData = await AreaChartDataCalculate(byTime);
            return Helper.RenderRazorViewToString(this, "_AreaChart",
                new AreaChartViewModel()
                {
                    dates = listData.ElementAt(1),
                    totalRevenueSeries = listData.ElementAt(1),
                    refundedSeries = listData.ElementAt(3),
                    incomingSeries = listData.ElementAt(4),
                    ByTime = byTime ?? ByTime.Today
                });
        }

        [HttpGet]
        [Route("FitlerTotalClasses/{byTime?}")]
        public async Task<IActionResult> FitlerTotalClasses(string? byTime)
        {
            _logger.LogDebug("Index's running! On getting classDtos...");
            var classDtos = await _sender.Send(new GetAllCoursesQuery());
            _logger.LogDebug("Got classDtos!");

            var date = GetByTime(DateTime.Now, byTime);
            var result1 = classDtos.Value.Where(x => x.CreationTime >= date).ToList();
            var date2 = GetByTime(date, byTime);
            var result2 = classDtos.Value.Where(x => x.CreationTime >= date2 && x.CreationTime <= date).ToList();

            return Helper.RenderRazorViewToString(this, "_TotalClasses", new TotalValueModel<CourseForListDto>()
            {
                Models = result1,
                IsIncrease = result1.Count > result2.Count,
                IncreasePercentage = (Math.Abs(result1.Count - result2.Count) * 1.0 / result2.Count) * 100,
                Time = byTime ?? "Today"
            });
        }

        [HttpGet]
        [Route("FilterTotalTutors/{byTime?}")]
        public async Task<IActionResult> FilterTotalTutors(string? byTime)
        {
            _logger.LogDebug("Index's running! On getting tutorDtos...");
            var tutorDtos = await _sender.Send(new GetAllTutorInformationsAdvancedQuery());
            _logger.LogDebug("Got tutorDtos!");
            var date = GetByTime(DateTime.Now, byTime);
            var result1 = tutorDtos.Value.Where(x => x.CreationTime >= date).ToList();
            var date2 = GetByTime(date, byTime);

            var result2 = tutorDtos.Value.Where(x => x.CreationTime >= date2 && x.CreationTime <= date).ToList();

            return Helper.RenderRazorViewToString(this, "_TotalTutors", new TotalValueModel<TutorForListDto>()
            {
                Models = result1,
                IsIncrease = result1.Count > result2.Count,
                IncreasePercentage = Math.Abs(result1.Count - result2.Count) * 1.0 / result2.Count * 100,
                Time = byTime ?? "Today"
            });
        }

        [HttpGet]
        [Route("FitlerTotalStudents/{byTime?}")]
        public async Task<IActionResult> FitlerTotalStudents(string? byTime)
        {
            _logger.LogDebug("Index's running! On getting studentDtos...");
            var studentDtos = await _sender.Send(new GetObjectQuery<PaginatedList<LearnerDto>>());
            _logger.LogDebug("Got studentDtos!");

            var date = GetByTime(DateTime.Now, byTime);
            var result1 = studentDtos.Value.Where(x => x.CreationTime >= date).ToList();
            var date2 = GetByTime(date, byTime);
            var result2 = studentDtos.Value.Where(x => x.CreationTime >= date2 && x.CreationTime <= date).ToList();


            return Helper.RenderRazorViewToString(this, "_TotalStudents", new TotalValueModel<LearnerDto>()
            {
                Models = result1,
                IsIncrease = result1.Count > result2.Count,
                IncreasePercentage = Math.Abs(result1.Count - result2.Count) * 1.0 / result2.Count * 100,
                Time = byTime ?? "Today"
            });
        }

        private DateTime GetByTime(DateTime date, string? byTime)
        {
            switch (byTime)
            {
                case ByTime.Month:
                    date = date.Subtract(TimeSpan.FromDays(29));
                    break;
                case ByTime.Week:
                    date = date.Subtract(TimeSpan.FromDays(6));
                    break;
                case ByTime.Year:
                    date = date.Subtract(TimeSpan.FromDays(364));
                    break;
                default:
                    if (date.Hour == 0)
                    {
                        return date.Subtract(TimeSpan.FromDays(1));
                    }
                    else
                    {
                        date = date.Subtract(date.TimeOfDay);
                    }

                    break;
            }

            return date;
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error")]
        public IActionResult Error()
        {
            
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}