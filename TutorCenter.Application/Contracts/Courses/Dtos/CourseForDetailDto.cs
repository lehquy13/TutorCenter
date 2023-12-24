﻿namespace TutorCenter.Application.Contracts.Courses.Dtos;

public class CourseForDetailDto
{
    //List of Request

    public List<CourseRequestForDetailDto> RequestGettingClassDtos = new();

    public string ReviewDetailDto { get; set; } = string.Empty;

    //Basic Information
    public int Id { get; set; } = 0;
    public DateTime CreationTime { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; }
    public DateTime? LastModificationTime { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "OnVerifying";
    public string LearningMode { get; set; } = "Offline";

    public float Fee { get; set; } = 0;
    public float ChargeFee { get; set; } = 0;

    //Tutor related information
    public string GenderRequirement { get; set; } = "None";
    public string AcademicLevelRequirement { get; set; } = "Optional";

    //Student related information
    public string LearnerName { get; set; } = "";
    public string LearnerGender { get; set; } = "None";
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
    public string SubjectName { get; set; } = string.Empty;

    //Confirmed data related
    public int? TutorId { get; set; }
    public string TutorName { get; set; } = string.Empty;
    public string TutorPhoneNumber { get; set; } = string.Empty;
    public string TutorEmail { get; set; } = string.Empty;
}