using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Contracts.Models;

namespace TutorCenter.Application.Contracts.Users.Tutors;

public class TutorForProfileDto : BasicAuditedEntityDto<int>
{
    public PaginatedList<CourseForListDto> CourseForListDtos = new();

    public PaginatedList<CourseRequestDto> CourseRequestDtos = new();

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

    //is tutor related informtions
    public string Role { get; set; } = "Tutor";
    public string AcademicLevel { get; set; } = "Student";
    public string University { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public short Rate { get; set; } = 5;
    public List<SubjectDto> Majors { get; set; } = new();
    public List<TutorVerificationInfoDto> TutorVerificationInfoDtos { get; set; } = new();
    public PaginatedList<ReviewDetailDto> ReviewDetailDtos { get; set; } = new();
}