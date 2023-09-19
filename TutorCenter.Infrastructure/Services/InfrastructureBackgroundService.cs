using System.Text;
using EduSmart.Domain.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Interfaces.Services;
using TutorCenter.Domain.Subscribers;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Infrastructure.Services;
/// <summary>
/// TODO: Too many hard code
/// </summary>
public class InfrastructureBackgroundService : BackgroundService
{
    private readonly PeriodicTimer _periodicTimer;
    private readonly ILogger<InfrastructureBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;



    public InfrastructureBackgroundService(ILogger<InfrastructureBackgroundService> logger,
        IServiceProvider serviceProvider)
    {
        
        _logger = logger;
        _serviceProvider = serviceProvider;
        
        //handling datetime
        TimeSpan timeSpan = TimeSpan.FromHours(20);
        if (DateTime.Now > DateTime.Today.AddHours(20)) //after 20pm
        {
            timeSpan += DateTime.Today.AddDays(1) - DateTime.Now;
        }
        else //before 20pm
        {
            timeSpan = DateTime.Today.Add(timeSpan) - DateTime.Now;
        }
         
        _periodicTimer = new PeriodicTimer(timeSpan);

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _periodicTimer.WaitForNextTickAsync(stoppingToken)
               && !stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();

            var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
            var classRepository = scope.ServiceProvider.GetRequiredService<IRepository<Course>>();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            //Get today class
            _logger.LogInformation("Getting today class");
            var todayClass = classRepository.GetAll()
                .Where(x => x.CreationTime >= DateTime.Today)
                .GroupBy(x => x.SubjectId)
                .Select(x => new
                {
                    subjectId = x.Key,
                    classInfo = x.ToList()

                })
                .ToList();

            var subscriberRepository = scope.ServiceProvider.GetRequiredService<IRepository<Subscriber>>();
            
            _logger.LogInformation("Getting subscriberEmails");
            var subscriberEmails = (await subscriberRepository.GetAllList())
                .Join(
                    (await userRepository.GetAllList()),
                    sub => sub.TutorId,
                    user => user.Id,
                    (sub, user) => new
                    {
                        TutorId = sub.TutorId,
                        TutorEmail = user.Email
                    });

            // find their major
            _logger.LogInformation("Getting subscriberEmails' majors");

            var major = scope.ServiceProvider.GetRequiredService<IRepository<TutorMajor>>();
            var subscriberWithMajors = subscriberEmails.GroupJoin(
                await major.GetAllList(),
                sub => sub.TutorId,
                maj => maj.TutorId,
                (sub, maj) => new
                {
                    subscriber = sub.TutorId,
                    subscriberMail = sub.TutorEmail,
                    subMajors = maj.Select(x => x.SubjectId).ToList()
                }
            );
            
             
            // traverse every subscriberWithMajors, collect new class and send to that subscriber
            foreach (var i in subscriberWithMajors)
            {
                var realTodayClasses = i.subMajors.Join(
                    todayClass,
                    subMajor => subMajor,
                    todayC => todayC.subjectId,
                    (subMajor, todayC) => todayC.classInfo
                ).ToList();
                if( realTodayClasses.Count <= 0) continue;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<div class=\"section\">\r\n    <div class=\"card\">\r\n        <div class=\"card-body\">");
                foreach (var classInformations in realTodayClasses)
                {
                    foreach(var classInformation in classInformations)
                    {
                        stringBuilder.Append($" <h3><a asp-controller=\"ClassInformation\" asp-action=\"Detail\" asp-route-id=\"{classInformation.Id}\">{classInformation.Title}</a></h3>\r\n\r\n                <div class=\"trainer d-flex justify-content-between align-items-center\">\r\n                    <ul class=\"row requestList\">\r\n                        <li class=\"col-sm-4\"><i class=\"far fa-clock text-danger mr-10\"></i> <b>Created:</b> 14:10 16.05.2023</li>\r\n\r\n                        <li class=\"col-sm-4\">\r\n                            <i class=\"fas fa-transgender text-danger mr-10\"></i> <b>Gender Requirements:</b>\r\n                            {classInformation.GenderRequirement}\r\n                        </li>\r\n\r\n                        <li class=\"col-sm-4\">\r\n                            <i class=\"far fa-calendar-alt text-danger mr-10\"></i><b>Academic requirements:</b> {classInformation.AcademicLevelRequirement}\r\n                        </li>\r\n\r\n\r\n                        <li class=\"col-sm-12\">\r\n                            <i class=\"fas fa-map-marker-alt text-danger mr-10\"></i>\r\n                            <b>Address:</b> {classInformation.Address}\r\n                            <a href=\"https://www.google.com/maps?q={classInformation.Address}\" class=\"text-danger\" target=\"_blank\">\r\n                                <small>\r\n                                    <em>(View map <i class=\"bi bi-map\"></i>)</em>\r\n                                </small>\r\n                            </a>\r\n                        </li>\r\n                    </ul>\r\n                </div>");
                    }
                }
                stringBuilder.Append("\r\n        </div>\r\n    </div>\r\n</div>");
                
                await emailSender.SendHtmlEmail("hoangle.q3@gmail.com", "EduSmart Center: Today's class", stringBuilder.ToString());
            }

            //....
        }
    }
}