﻿using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Contracts.Models;
using TutorCenter.Domain.ClassInformationConsts;

namespace TutorCenter.Application.Contracts.Users.Learners;

public class LearnerDto : FullAuditedAggregateRootDto<int>
{
    //Admin information
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; } = Gender.Male;
    public int BirthYear { get; set; } = 1960;

    public string Image { get; set; } =
        @"https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";
//    public string WardId { get; set; } = "00001";

    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;


    //Account References
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; } = false;

    public string PhoneNumber { get; set; } = string.Empty;
    //public string Password { get; set; } = "1q2w3E*";

    //is tutor related informtions
    public UserRole Role { get; set; } = UserRole.Learner;

    public PaginatedList<CourseForListDto> LearningCourses { get; set; } = new();
}