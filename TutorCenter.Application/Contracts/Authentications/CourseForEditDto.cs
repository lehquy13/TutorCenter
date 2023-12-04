namespace TutorCenter.Application.Contracts.Authentications;

public class CourseForEditDto
{
    public int Id { get; set; }

    //User information
    public string FullName { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    //Account References
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "Learner";

    public DateTime LastModificationTime { get; set; }
}