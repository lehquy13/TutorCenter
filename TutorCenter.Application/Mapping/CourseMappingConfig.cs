using Mapster;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Domain.Courses;

namespace TutorCenter.Application.Mapping;

public class CourseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Config for Request getting class
        config.NewConfig<Course, LearningCourseForListDto>()
            .Map(dest => dest.TutorName, src => src.CourseRequests.FirstOrDefault()!.Tutor.GetFullNAme(),  srcCon => srcCon.CourseRequests.Any())
            .Map(dest => dest.TutorName, src => "",  srcCon => !srcCon.CourseRequests.Any())
            .Map(dest => dest.TutorId, src => src.CourseRequests.FirstOrDefault()!.Tutor.Id , srcCon => srcCon.CourseRequests.Any())
            .Map(dest => dest.TutorId, src => 0 , srcCon => !srcCon.CourseRequests.Any())
            .Map(dest => dest.TutorPhoneNumber, src => src.CourseRequests.FirstOrDefault()!.Tutor.PhoneNumber,  srcCon => srcCon.CourseRequests.Any())
            .Map(dest => dest.TutorPhoneNumber, src => "",  srcCon => !srcCon.CourseRequests.Any())
            .Map(dest => dest, src => src);

        config.NewConfig<CourseRequest, CourseRequestDto>();

        config.NewConfig<CourseRequest, CourseRequestForDetailDto>()
            .Map(dest => dest.RequestStatus, src => src.RequestStatus.ToString())
            .Map(dest => dest.LearnerContact, src => src.Course.ContactNumber)
            .Map(dest => dest.LearnerName, src => src.Course.LearnerName)
            .Map(dest => dest.Title, src => src.Course.Title)
            .Map(dest => dest.TutorEmail, src => src.Tutor.Email)
            .Map(dest => dest.TutorPhone, src => src.Tutor.PhoneNumber)
            .Map(dest => dest.TutorName, src => src.Tutor.GetFullNAme())
            .Map(dest => dest, src => src);
        config.NewConfig<CourseRequest, CourseRequestForListDto>()
            .Map(dest => dest.RequestStatus, src => src.RequestStatus.ToString())
            .Map(dest => dest.SubjectName, src => src.Course.Subject.Name)
            .Map(dest => dest.Title, src => src.Course.Title)
            .Map(dest => dest, src => src);

        config.NewConfig<CourseRequestForDetailDto, CourseRequest>();
        //Config for Tutor review
        config.NewConfig<ReviewDetail, ReviewDetailDto>();
        config.NewConfig<ReviewDetailDto, ReviewDetail>();
       
        
        //Config for Class Information
        config.NewConfig<CourseDto, Course>()        
            //.Map(dest => dest.TutorId, src => src.TutorId)
            .Map(dest => dest, src => src);
        config.NewConfig<Course, CourseDto >()        
            //.Map(dest => dest.TutorId, src => src.TutorId)
            .Map(dest => dest, src => src);
        config.NewConfig<Course, CourseForDetailDto>()
            .Map(dest => dest.TutorName, src => src.CourseRequests.FirstOrDefault()!.Tutor.GetFullNAme(),  srcCon => srcCon.CourseRequests.Any())
            .Map(dest => dest.TutorName, src => "",  srcCon => !srcCon.CourseRequests.Any())
            .Map(dest => dest.TutorId, src => src.CourseRequests.FirstOrDefault()!.Tutor.Id , srcCon => srcCon.CourseRequests.Any())
            .Map(dest => dest.TutorId, src => 0 , srcCon => !srcCon.CourseRequests.Any())
            .Map(dest => dest.TutorPhoneNumber, src => src.CourseRequests.FirstOrDefault()!.Tutor.PhoneNumber,  srcCon => srcCon.CourseRequests.Any())
            .Map(dest => dest.TutorPhoneNumber, src => "",  srcCon => !srcCon.CourseRequests.Any())
            .Map(dest => dest, src => src);
        config.NewConfig<Course, CourseForListDto>();


        //Config for ReviewDetail
        config.NewConfig<ReviewDetailDto, ReviewDetail>();
       
      
        config.NewConfig<Subject, SubjectDto>();
        config.NewConfig<SubjectDto, Subject>();


    }
}

