﻿using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Contracts.Models;
using TutorCenter.Application.Contracts.TutorReview;
using TutorCenter.Domain.ClassInformationConsts;

namespace TutorCenter.Application.Contracts.Users.Tutors;

public class TutorForDetailDto : FullAuditedAggregateRootDto<int>
{
    //Admin information
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = "Male";
    public int BirthYear { get; set; } = 1960;
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string Image { get; set; } =
        @"https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";

    //Account References
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; } = false;

    public string PhoneNumber { get; set; } = string.Empty;

    //Tutor's related informations
    public UserRole Role { get; set; } = UserRole.Tutor;
    public string AcademicLevel { get; set; } = "Student";
    public string University { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public short Rate { get; set; } = 5;
    public List<SubjectDto> Majors { get; set; } = new();
    public PaginatedList<ReviewDetailDto> ReviewDetailDtos { get; set; } = new();

    public List<TutorVerificationInfoDto> TutorVerificationInfoDtos { get; set; } = new();
    public PaginatedList<TutorReviewDto> TutorReviewDtos { get; set; } = new();
    public PaginatedList<RequestGettingClassForListDto> RequestGettingClassForListDtos = new();
}