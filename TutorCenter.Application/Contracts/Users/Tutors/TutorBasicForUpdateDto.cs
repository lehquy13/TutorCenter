namespace TutorCenter.Application.Contracts.Users.Tutors;
public class TutorBasicForUpdateDto 
{
    public int Id { get; set; }
    //is tutor related informtions
    public string AcademicLevel { get; set; } = "Student";
    public string University { get; set; } = string.Empty;
    public List<int> Majors { get; set; } = new();
    public List<TutorVerificationInfoDto> TutorVerificationInfoDtos { get; set; } = new();
}

public class TutorBasicForRegisterCommand
{
    public int Id { get; set; }
    //is tutor related informtions
    public string AcademicLevel { get; set; } = "Student";
    public string University { get; set; } = string.Empty;
    public List<string> Majors { get; set; } = new();
    public List<string> TutorVerificationInfoDtos { get; set; } = new();
    public Stream? Stream { get; set; } 


}
