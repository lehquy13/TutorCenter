using System.ComponentModel.DataAnnotations;
using TutorCenter.Application.Contracts.Models;

namespace TutorCenter.Application.Contracts.Courses.Dtos;

public class CourseForCreatingDto : BasicAuditedEntityDto<int>
{
    [Required] [StringLength(128)] public string Title { get; set; } = string.Empty;

    [Required] public string Description { get; set; } = string.Empty;

    [Required] public string Status { get; set; } = "OnVerifying";

    [Required] public string LearningMode { get; set; } = "Offline";

    [Required] public float Fee { get; set; } = 0;

    [Required] public float ChargeFee { get; set; } = 0;

    //Tutor related information
    [Required] public string GenderRequirement { get; set; } = "None";

    [Required] public string AcademicLevel { get; set; } = "Optional";

    //Learner related information
    [Required] public string LearnerGender { get; set; } = "Male";

    [Required] public int NumberOfLearner { get; set; } = 1;

    [Required] public string ContactNumber { get; set; } = string.Empty;

    public int? LearnerId { get; set; }


    // Time related information
    [Required] public int MinutePerSession { get; set; } = 90;

    [Required] public int SessionPerWeek { get; set; } = 2;


    // Address related information
    [Required] public string Address { get; set; } = string.Empty;

    //Subject related information
    [Required] public int SubjectId { get; set; }

    public string SubjectName { get; set; } = string.Empty;
}