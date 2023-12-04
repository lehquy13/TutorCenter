using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Contracts.Models;
using TutorCenter.Application.Contracts.TutorReview;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Domain.ClassInformationConsts;

namespace TutorCenter.Application.Contracts.Users;

public class UserForDetailDto : BasicAuditedEntityDto<int>
{
    //User information
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = "Male";

    public int BirthYear { get; set; } = 1960;

    //public string WardId { get; set; } = "00001";
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;


    //Account References
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; } = false;

    public string PhoneNumber { get; set; } = string.Empty;

    public string? Password { get; set; } = "1q2w3E*";

    //is tutor related informtions

    //Account References

    //is tutor related informtions
    public UserRole Role { get; set; } = UserRole.Tutor;
    public AcademicLevel AcademicLevel { get; set; } = AcademicLevel.Student;
    public string University { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public short Rate { get; set; } = 5;
    public List<SubjectDto> Majors { get; set; } = new();
    public List<TutorVerificationInfoDto> TutorVerificationInfoDtos { get; set; } = new();
    public PaginatedList<TutorReviewDto> TutorReviewDtos { get; set; } = new();
    public PaginatedList<RequestGettingClassForListDto> RequestGettingClassForListDtos = new();
}