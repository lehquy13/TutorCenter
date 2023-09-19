using TutorCenter.Application.Contracts.Models;

namespace TutorCenter.Application.Contracts.Users.Tutors;
public class TutorForListDto : BasicAuditedEntityDto<int>
{
    //Admin information
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = "Male";
    public int BirthYear { get; set; } = 1960;
    //public string WardId { get; set; } = "00001";
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = @"https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";

    //Account References
    public string PhoneNumber { get; set; } = string.Empty;

    //is tutor related informtions
    public string AcademicLevel { get; set; } ="Student";
    public string University { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public short Rate { get; set; } = 5;

}