using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Contracts.Models;

namespace TutorCenter.Application.Contracts.Users.Learners;

public class LearnerForUpdateDto : FullAuditedAggregateRootDto<int>
{
    //Admin information
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = "Male";
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

    //is tutor related informtions
    public string Role { get; set; } = "Learner";

    public PaginatedList<CourseForListDto> LearningCourses { get; set; } = new();
}