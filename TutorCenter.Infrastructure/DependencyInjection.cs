using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Interfaces.Authentication;
using TutorCenter.Domain.Interfaces.Logger;
using TutorCenter.Domain.Interfaces.Services;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users.Repos;
using TutorCenter.Infrastructure.Authentication;
using TutorCenter.Infrastructure.Entity_Framework_Core;
using TutorCenter.Infrastructure.Logging;
using TutorCenter.Infrastructure.Persistence;
using TutorCenter.Infrastructure.Persistence.Repository;
using TutorCenter.Infrastructure.Services;
using TutorCenter.Infrastructure.Services.EmailServices;

namespace TutorCenter.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration
        )
        {
            // Authentication configuration using jwt bearer
            services.AddAuth(configuration);

            // set configuration settings to emailSettingName and turn it into Singleton
            var emailSettingNames = new EmailSettingNames();
            configuration.Bind(EmailSettingNames._SectionName, emailSettingNames);
            services.AddSingleton(Options.Create(emailSettingNames));
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                )
            );
            
          
            
            //Configure DI for Infrastructure services
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

                //services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));


            // Dependency Injection for repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseRequestRepository, CourseRequestRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITutorRepository, TutorRepository>();
            
            // set configuration settings to cloudinarySettings and turn it into Singleton
            var cloudinary = new CloudinarySetting();
            configuration.Bind(CloudinarySetting._SectionName, cloudinary);
            services.AddSingleton(Options.Create(cloudinary));
            services.AddScoped<ICloudinaryFile, CloudinaryFile>();
            services.AddScoped<IEmailSender, EmailSender>();
            //configure BackgroundService
            services.AddHostedService<InfrastructureBackgroundService>();
            return services;
        }

        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            ConfigurationManager configuration
        )
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IValidator, Validator>();

            // set configuration settings to jwtSettings and turn it into Singleton
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings._SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));

            //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings._SectionName));
            
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(480);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            
            services.AddAuthentication(scheme =>
                {
                    scheme.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    scheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secrect)),
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            try
                            {
                                var token = context.HttpContext.Session.GetString("access_token");
                                if (token != null)
                                {
                                    context.Token = token;
                                }
                                
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            return Task.CompletedTask;
                        },
                    
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy =>
                {
                    policy.RequireRole(UserRole.Admin.ToString());
                });
                options.AddPolicy("RequireTutorRole", policy =>
                {
                    policy.RequireRole(UserRole.Tutor.ToString());
                });
 
            });

            return services;
        }
    }
}