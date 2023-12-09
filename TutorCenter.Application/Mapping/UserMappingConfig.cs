using Mapster;
using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Contracts.Users.Learners;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Domain;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Users;

namespace TutorCenter.Application.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, LearnerDto>();
        config.NewConfig<User, UserLoginDto>()
            .Map(des => des.FullName, src => src.GetFullNAme())
            .Map(des => des, src => src);
        config.NewConfig<User, Tutor>();
        config.NewConfig<User, LearnerForProfileDto>()
            .Map(des => des.Gender, src => src.Gender.ToString())
            .Map(des => des.CourseForListDtos, src => src.LearningCourses)
            .Map(des => des, src => src);
        //Map when updating profile of learner
        config.NewConfig<LearnerForCreateUpdateDto, User>()
            .Map(des => des.Gender, src => src.Gender.ToEnum<Gender>())
            .Map(des => des.Role, src => src.Role.ToEnum<UserRole>())
            .Map(des => des.FirstName, src => src.FirstName)
            .Map(des => des.LastName, src => src.LastName)
            .Map(des => des.BirthYear, src => src.BirthYear)
            .Map(des => des.Address, src => src.Address)
            .Map(des => des.Description, src => src.Description)
            .Map(des => des.Email, src => src.Email)
            .Map(des => des.PhoneNumber, src => src.PhoneNumber)
            .IgnoreNonMapped(true);
        //Map when updating profile of tutor
        config.NewConfig<TutorBasicForUpdateDto, Tutor>()
            .Map(des => des.AcademicLevel, src => src.AcademicLevel.ToEnum<AcademicLevel>())
            .Map(des => des.University, src => src.University)
            .Map(des => des.IsVerified, src => false)
            .IgnoreNonMapped(true);


        config.NewConfig<Tutor, TutorBasicDto>()
            .Map(des => des.Role, src => src.Role.ToString())
            .Map(des => des.AcademicLevel, src => src.AcademicLevel.ToString())
            .Map(des => des, src => src);

        config.NewConfig<ReviewDetail, ReviewDetailDto>();
        config.NewConfig<Subject, SubjectDto>();
        config.NewConfig<Tutor, TutorForDetailDto>()
            .Map(des => des.Majors, src => src.Subjects.ToList())
            .Map(des => des.Gender, src => src.Gender.ToString())
            .Map(des => des.Role, src => src.Role.ToString())
            .Map(des => des.AcademicLevel, src => src.AcademicLevel.ToString())
            .Map(des => des, src => src);
        config.NewConfig<TutorVerificationInfo, TutorVerificationInfoDto>();

        config.NewConfig<Tutor, TutorBasicForUpdateDto>()
            .Map(des => des.Majors, src => src.Subjects.Select(x => x.Id).ToList())
            .Map(des => des.TutorVerificationInfoDtos, src => src.TutorVerificationInfos)
            .Map(des => des, src => src);
        config.NewConfig<Tutor, TutorForProfileDto>()
            .Map(des => des.Majors, src => src.Subjects.Select(x => x.Name).ToList())
            .Map(des => des.Gender, src => src.Gender.ToString())
            .Map(des => des.Role, src => src.Role.ToString())
            .Map(des => des.AcademicLevel, src => src.AcademicLevel.ToString())
            .Map(des => des.TutorVerificationInfoDtos, src => src.TutorVerificationInfos)
            .Map(des => des.CourseRequestDtos, src => src.CourseRequests)
            .Map(des => des, src => src);


        config.NewConfig<Tutor, TutorForCreateUpdateDto>();
        // config.NewConfig<LearnerDto, TutorForDetailDto>();
        // config.NewConfig<User, UserDto>();
        // config.NewConfig<User, LearnerDto>();
        // config.NewConfig<UserDto, User>()
        //     .Map(des => des.FirstName, src => src.FirstName)
        //     .Map(des => des.LastName, src => src.LastName)
        //     .Map(des => des.Gender, src => src.Gender)
        //     .Map(des => des.BirthYear, src => src.BirthYear)
        //     .Map(des => des.Address, src => src.Address)
        //     .Map(des => des.Description, src => src.Description)
        //     .Map(des => des.Email, src => src.Email)
        //     .Map(des => des.PhoneNumber, src => src.PhoneNumber)
        //     .Map(des => des.Role, src => src.Role)
        //     .Ignore(des => des.Password);
        //
        // config.NewConfig<TutorForDetailDto, Tutor>()
        //     .Map(des => des.AcademicLevel, src => src.AcademicLevel)
        //     .Map(des => des.University, src => src.University)
        //     .Map(des => des.IsVerified, src => src.IsVerified)
        //     .Map(des => des.Rate, src => src.Rate);
        // config.NewConfig<TutorVerificationInfo, TutorVerificationInfo>()
        //     .Map(des => des.TutorId, src => src.Id)
        //     .Map(des => des.Image, src => src.Image);
        // //TODO: Check does this work well?
        // config.NewConfig<Tutor, TutorForDetailDto>()
        //     .Map(des => des.TutorReviewDtos, src => src.RequestGettingClasses.Select(x => x.ClassInformation.TutorReviews))
        //     .Map(des => des.Majors, src => src.Subjects)
        //     .Map(des => des, src => src);
        // config.NewConfig<Tutor, TutorForListDto>();
        // config.NewConfig<TutorForRegistrationDto, Tutor>();
        // config.NewConfig<User, Tutor>();
        //
        //
        //
        // config.NewConfig<(User, ClassInformation), LearnerDto>()
        //     .Map(des => des.LearningClassInformations, src => src.Item2)
        //     .Map(des => des, src => src.Item1);
        //
        // //These configs are mainly used for mapping from tutor profile 
        // config.NewConfig<TutorVerificationInfo, TutorVerificationInfoDto>();
        // config.NewConfig<RequestGettingClass, RequestGettingClassForListDto>();
        // config.NewConfig<Tutor, TutorProfileDto>()
        //     .Map(des => des.RequestGettingClassForListDtos, src => src.RequestGettingClasses)
        //     .Map(des => des.TutorMainInfoDto.Majors , src => src.Subjects)
        //     .Map(des => des.TutorMainInfoDto.TutorVerificationInfoDtos, src => src.TutorVerificationInfos)
        //     .Map(des => des.TutorMainInfoDto, src => src);
    }
}