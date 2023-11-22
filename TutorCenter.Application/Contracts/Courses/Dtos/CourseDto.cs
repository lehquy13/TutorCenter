﻿using TutorCenter.Application.Contracts.Models;
using TutorCenter.Domain.ClassInformationConsts;

namespace TutorCenter.Application.Contracts.Courses.Dtos;

public class CourseDto : FullAuditedAggregateRootDto<int>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.OnVerifying;
    public LearningMode LearningMode { get; set; } = LearningMode.Offline;

    public float Fee { get; set; } = 0;
    public float ChargeFee { get; set; } = 0;

    //Tutor related information

    public Gender GenderRequirement { get; set; } = Gender.None;
    public AcademicLevel AcademicLevelRequirement { get; set; } = AcademicLevel.Optional;

    //Learner related information
    public string LearnerName { get; set; } = string.Empty;
    public Gender LearnerGender { get; set; } = Gender.Male;
    public int NumberOfLearner { get; set; } = 1;
    public string ContactNumber { get; set; } = string.Empty;
    public int? LearnerId { get; set; }

    // Time related information
    public int MinutePerSession { get; set; } = 90;
    public int SessionPerWeek { get; set; } = 2;

    // Address related information
    public string Address { get; set; } = string.Empty;

    //Subject related information
    public int SubjectId { get; set; }
    public SubjectDto SubjectDto { get; set; } = null!;


    //Request of class
    public List<CourseRequestDto> CourseRequestDtos { get; set; } = new();
    public ReviewDetailDto ReviewDetailDtos { get; set; } = new();
}