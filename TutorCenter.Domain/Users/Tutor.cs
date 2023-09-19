using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses;

namespace TutorCenter.Domain.Users;

public class Tutor : User
{
    //is tutor related informtions
    public AcademicLevel AcademicLevel { get; set; } = AcademicLevel.Student;
    public string? University { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public short Rate { get; set; } = 5;

    public ICollection<Subject> Subjects { get; set; } = null!;
    public ICollection<TutorVerificationInfo> TutorVerificationInfos { get; set; } = null!;
    public ICollection<CourseRequest> CourseRequests { get; set; } = null!;
    /// <summary>
    /// Update tutor's information and change the state into being verified
    /// </summary>
    /// <param name="tutor"></param>
    public void UpdateTutorInformation(Tutor tutor)
    {
        AcademicLevel = tutor.AcademicLevel;
        University = tutor.University;

        //wait for being verified
        IsVerified = false;
    }
    public void VerifyTutorInformation(Tutor tutor)
    {
        UpdateUserInformation(tutor);
        UpdateTutorInformation(tutor);
        IsVerified = true;
    }

   
}
