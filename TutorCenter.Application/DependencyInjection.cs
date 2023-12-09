using System.Reflection;
using FluentResults;
using FluentValidation;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using TutorCenter.Application.Common.Behaviors;
using TutorCenter.Application.Common.Caching;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Mapping;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Application.Services.Courses.Queries.GetAllCoursesQuery;

namespace TutorCenter.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddApplicationMappings();
        services.AddMediatR(
            cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.NotificationPublisher = new TaskWhenAllPublisher();
            });
        ;
        services.AddLazyCache();


        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
       
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}